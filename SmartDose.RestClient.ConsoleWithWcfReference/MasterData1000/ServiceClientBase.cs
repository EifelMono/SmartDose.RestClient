﻿using System;
using System.ServiceModel;
using System.Threading.Tasks;
using SmartDose.Core;
using SmartDose.Core.Extensions;

namespace MasterData1000
{
    public abstract class ServiceClientBase : IDisposable
    {
        public string EndpointAddress { get; protected set; }
        public SecurityMode SecurityMode { get; protected set; }
        // Timeout?!

        public bool ThrowOnConnectionError { get; set; } = false;

        public ServiceClientBase(string endpointAddress, SecurityMode securityMode = SecurityMode.None)
        {
            EndpointAddress = endpointAddress;
            SecurityMode = securityMode;
            Run();
        }

        protected bool Disposed = false;
        public void Dispose()
        {
            if (!Disposed)
                QueuedEvent.New(ClientEvent.Dispose);
        }

        #region Client
        public ICommunicationObject Client { get; set; }

        public enum ClientEvent
        {
            None,

            Opening,
            Opened,
            Faulted,
            Closing,
            Closed,

            Exception,
            Dispose,

            Restart,
        }
        public QueuedEvent<ClientEvent> QueuedEvent { get; set; } = new QueuedEvent<ClientEvent>();

        public abstract void CreateClient();

        #region Client Abstract 
        public abstract Task OpenAsync();

        public abstract Task CloseAsync();

        public abstract Task SubscribeForCallbacksAsync();

        public abstract Task UnsubscribeForCallbacksAsync();
        #endregion

        #region Client Events

        public event Action<ClientEvent> OnClientEvent;
        protected void AssignClientEvents(bool on)
        {
            if (Client is null)
                return;
            switch (on)
            {
                case true:
                    Client.Opening += Client_Opening;
                    Client.Opened += Client_Opened;
                    Client.Faulted += Client_Faulted;
                    Client.Closing += Client_Closing;
                    Client.Closed += Client_Closed;
                    break;
                case false:
                    Client.Opening -= Client_Opening;
                    Client.Opened -= Client_Opened;
                    Client.Faulted -= Client_Faulted;
                    Client.Closing -= Client_Closing;
                    Client.Closed -= Client_Closed;
                    break;
            }
        }

        private void Client_Opening(object sender, EventArgs e)
            => QueuedEvent.New(ClientEvent.Opening);
        private void Client_Opened(object sender, EventArgs e)
            => QueuedEvent.New(ClientEvent.Opened);
        private void Client_Faulted(object sender, EventArgs e)
         => QueuedEvent.New(ClientEvent.Faulted);
        private void Client_Closing(object sender, EventArgs e)
            => QueuedEvent.New(ClientEvent.Closing);
        private void Client_Closed(object sender, EventArgs e)
            => QueuedEvent.New(ClientEvent.Closed);
        #endregion

        #region Client Run
        public bool IsOpened { get; set; } = false;
        protected virtual void Run()
        {
            Task.Run(async () =>
            {
                try
                {
                    QueuedEvent.OnNew = (e) =>
                    {
                        OnClientEvent?.Invoke(e);
                    };
                    var inFault = false;
                    var running = true;
                    RunOpen();
                    while (running)
                    {
                        if (await QueuedEvent.Next() is var nextEvent && nextEvent.Ok)
                            switch (nextEvent.Value)
                            {
                                case ClientEvent.Dispose:
                                    RunClose();
                                    running = false;
                                    break;
                                case ClientEvent.Opening:
                                    break;
                                case ClientEvent.Opened:
                                    IsOpened = true;
                                    break;
                                case ClientEvent.Restart:
                                case ClientEvent.Faulted:
                                    if (!inFault)
                                    {
                                        inFault = true;
                                        RunClose();
                                        await Task.Delay(1000);
                                        RunOpen();
                                        inFault = false;
                                    }
                                    break;
                                case ClientEvent.Closing:
                                    break;
                                case ClientEvent.Closed:
                                    break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    ex.LogException();
                    QueuedEvent.New(ClientEvent.Exception);
                }
                finally
                {
                    RunClose();
                    Disposed = true;
                    QueuedEvent.OnNew = null;
                }
            });
        }

        protected void RunOpen()
        {
            CreateClient();
            AssignClientEvents(true);
            // InstallEvents();
            OpenAsync();
            SubscribeForCallbacksAsync().Wait();
        }

        protected void RunClose()
        {
            IsOpened = false;
            UnsubscribeForCallbacksAsync().Wait();
            CloseAsync();
            // UninstallEvents();
            AssignClientEvents(false);
        }
        #endregion
        #endregion

        #region SafeExecuter Catcher

        protected void Catcher(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
        }

        protected void CatcherAsTask(Action action)
        {
            Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    ex.LogException();
                }
            });
        }

        protected async Task<TResult> CatcherAsync<TResult>(Func<Task<TResult>> func) where TResult : ServiceResult, new()
        {
            try
            {
                return await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (ThrowOnConnectionError)
                    throw ex;
                return new TResult { Exception = ex, Status = ServiceResultStatus.ErrorConnection };
            }
        }

        protected async Task CatcherAsync(Func<Task> func)
        {
            try
            {
                await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (ThrowOnConnectionError)
                    throw ex;
            }
        }

        protected async Task CatcherAsyncIgnore(Func<Task> func)
        {
            try
            {
                await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (ThrowOnConnectionError)
                    throw ex;
            }
        }
        #endregion

        #region Query

        public abstract Task<ServiceResult> ExecuteQueryBuilderAsync(QueryBuilder queryBuilder);

        public QueryBuilder<T> NewQuery<T>() where T : class
        {
            return new QueryBuilder<T>(this) { };
        }
        #endregion
    }
}
