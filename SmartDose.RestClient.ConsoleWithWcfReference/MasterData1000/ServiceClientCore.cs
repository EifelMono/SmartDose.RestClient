using System;
using System.ServiceModel;
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
         
        }
        public void Dispose()
        {
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
        protected void ClientSetAllNotifyEvents(bool on)
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

        private void Client_Closing(object sender, EventArgs e)
            => FireClientNotifyEvent(ClientNotifyEvent.Closing);
        private void Client_Closed(object sender, EventArgs e)
            => FireClientNotifyEvent(ClientNotifyEvent.Closed);
        private void Client_Faulted(object sender, EventArgs e)
            => FireClientNotifyEvent(ClientNotifyEvent.Faulted);
        private void Client_Opened(object sender, EventArgs e)
            => FireClientNotifyEvent(ClientNotifyEvent.Opened);
        private void Client_Opening(object sender, EventArgs e)
            => FireClientNotifyEvent(ClientNotifyEvent.Opening);

        protected void FireClientNotifyEvent(ClientNotifyEvent clientNotifyEvent)
        {
            try
            {
                Task.Run(() =>
                {
                    try
                    {
                        ClientNotifyEventComletionSource.TrySetResult(clientNotifyEvent);
                        OnClientNotifyEvent.Invoke(clientNotifyEvent);
                    }
                    catch { }
                });
            }
            catch { }
        }

        protected object ClientNotifyEventComletionSourceLock = new object();
        protected TaskCompletionSource<ClientNotifyEvent> ClientNotifyEventComletionSource
                    = new TaskCompletionSource<ClientNotifyEvent>(TaskCreationOptions.RunContinuationsAsynchronously);

        protected void InitClientNotifyEventComletionSource()
        {
            lock (ClientNotifyEventComletionSourceLock)
                ClientNotifyEventComletionSource = new TaskCompletionSource<ClientNotifyEvent>();
        }
        #endregion

        #region Run
        public bool IsOpen { get; set; } = false;
        protected virtual void Run()
        {
            Task.Run(async () =>
            {
                try
                {
                    bool inFault = false;
                    bool running = true;
                    RunOpen();
                    while (running)
                    {
                        InitClientNotifyEventComletionSource();
                        var clientNotifyEvent = ClientNotifyEvent.None;
                        try { clientNotifyEvent = await ClientNotifyEventComletionSource.Task; }
                        catch { clientNotifyEvent = ClientNotifyEvent.None; }
                        switch (clientNotifyEvent)
                        {
                            case ClientNotifyEvent.Dispose:
                                RunClose();
                                running = false;
                                break;
                            case ClientNotifyEvent.Opening:
                                break;
                            case ClientNotifyEvent.Opened:
                                IsOpen = true;
                                break;
                            case ClientNotifyEvent.Faulted:
                                if (!inFault)
                                {
                                    IsOpen = false;
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
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.LogException();
                    FireClientNotifyEvent(ClientNotifyEvent.Exception);
                }
            });
        }

        public T RunStart<T>() where T: ServiceClientCore
        {
            Run();
            return (ServiceClientCore)this;
        }


        protected void RunOpen()
        {
            CreateClient();
            ClientSetAllNotifyEvents(true);
            // InstallEvents();
            OpenAsync();
            SubscribeForCallbacksAsync().Wait();
        }

        protected void RunClose()
        {
            UnsubscribeForCallbacksAsync().Wait();
            CloseAsync();
            // UninstallEvents();
            ClientSetAllNotifyEvents(false);
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
