using System;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.Core.Extensions;

namespace MasterData1000
{
    public abstract class ServiceClientCore : IDisposable
    {
        public enum ClientNotifyEvent
        {
            None,

            Opening,
            Opened,
            Faulted,
            Closing,
            Closed,

            Exception,
            Dispose,
        }

        public string EndpointAddress { get; protected set; }
        public SecurityMode SecurityMode { get; protected set; }
        // Timeout?!

        public bool ThrowOnConnectionError { get; set; }

        public ServiceClientCore(string endpointAddress, SecurityMode securityMode = SecurityMode.None)
        {
            EndpointAddress = endpointAddress;
            SecurityMode = securityMode;
            Run();
        }

        protected bool Disposed = false;
        public void Dispose()
        {
            if (!Disposed)
                FireClientNotifyEvent(ClientNotifyEvent.Dispose);
        }

        #region Client
        public ICommunicationObject Client { get; set; }

        public abstract void CreateClient();

        #region abstract 

        public abstract Task OpenAsync();

        public abstract Task CloseAsync();

        public abstract Task SubscribeForCallbacksAsync();

        public abstract Task UnsubscribeForCallbacksAsync();
        #endregion

        #region Events

        public event Action<ClientNotifyEvent> OnClientNotifyEvent;
        protected void AssignClientNotifyEvents(bool on)
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
            => FireClientNotifyEvent(ClientNotifyEvent.Opening);
        private void Client_Opened(object sender, EventArgs e)
            => FireClientNotifyEvent(ClientNotifyEvent.Opened);
        private void Client_Faulted(object sender, EventArgs e)
         => FireClientNotifyEvent(ClientNotifyEvent.Faulted);
        private void Client_Closing(object sender, EventArgs e)
            => FireClientNotifyEvent(ClientNotifyEvent.Closing);
        private void Client_Closed(object sender, EventArgs e)
            => FireClientNotifyEvent(ClientNotifyEvent.Closed);

        protected void FireClientNotifyEvent(ClientNotifyEvent clientNotifyEvent)
        {
            try
            {
                Task.Run(() =>
                {
                    try
                    {
                        ClientNotifyEventQueue.Enqueue(clientNotifyEvent);
                        var setResult = false;
                        lock (ClientNotifyEventCompletionSourceLock)
                            setResult = ClientNotifyEventCompletionSource.TrySetResult(true);
                        if (!setResult)
                            Task.Run(() =>
                            {
                                var tries = 0;
                                while (tries++ < 10)
                                {
                                    if (ClientNotifyEventCompletionSource.TrySetResult(true))
                                        break;
                                    Thread.Sleep(100);
                                }
                            });
                        OnClientNotifyEvent.Invoke(clientNotifyEvent);
                    }
                    catch { }
                });
            }
            catch { }
        }

        protected ConcurrentQueue<ClientNotifyEvent> ClientNotifyEventQueue = new ConcurrentQueue<ClientNotifyEvent>();
        protected object ClientNotifyEventCompletionSourceLock = new object();
        protected TaskCompletionSource<bool> ClientNotifyEventCompletionSource
                    = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        protected void InitClientNotifyEventCompletionSource()
        {
            lock (ClientNotifyEventCompletionSourceLock)
                ClientNotifyEventCompletionSource = new TaskCompletionSource<bool>();
        }
        #endregion

        #region Run
        public bool IsConnected { get; set; } = false;
        protected virtual void Run()
        {
            Task.Run(async () =>
            {
                try
                {
                    var inFault = false;
                    var running = true;
                    InitClientNotifyEventCompletionSource();
                    RunOpen();
                    while (running)
                    {
                        var clientNotifyEvent = ClientNotifyEvent.None;
                        if (!ClientNotifyEventQueue.TryDequeue(out clientNotifyEvent))
                        {
                            try { await ClientNotifyEventCompletionSource.Task; } catch { }
                            InitClientNotifyEventCompletionSource();
                            if (!ClientNotifyEventQueue.TryDequeue(out clientNotifyEvent))
                                clientNotifyEvent = ClientNotifyEvent.None;
                        }
                        switch (clientNotifyEvent)
                        {
                            case ClientNotifyEvent.Dispose:
                                RunClose();
                                running = false;
                                break;
                            case ClientNotifyEvent.Opening:
                                break;
                            case ClientNotifyEvent.Opened:
                                IsConnected = true;
                                break;
                            case ClientNotifyEvent.Faulted:
                                if (!inFault)
                                {
                                    IsConnected = false;
                                    inFault = true;
                                    RunClose();
                                    await Task.Delay(1000);
                                    RunOpen();
                                    inFault = false;
                                }
                                break;
                            case ClientNotifyEvent.Closing:
                                break;
                            case ClientNotifyEvent.Closed:
                                IsConnected = false;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.LogException();
                    FireClientNotifyEvent(ClientNotifyEvent.Exception);
                }
                RunClose();
                Disposed = true;
            });
        }

        protected void RunOpen()
        {
            CreateClient();
            AssignClientNotifyEvents(true);
            // InstallEvents();
            OpenAsync();
            SubscribeForCallbacksAsync().Wait();
        }

        protected void RunClose()
        {
            UnsubscribeForCallbacksAsync().Wait();
            CloseAsync();
            // UninstallEvents();
            AssignClientNotifyEvents(false);
        }
        #endregion
        #endregion

        #region SafeExecuter Catcher
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

        protected async Task CatcherAsync(Func<Task> func, bool ignoreError = false)
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
            return new QueryBuilder<T> { Client = this };
        }
        #endregion
    }
}
