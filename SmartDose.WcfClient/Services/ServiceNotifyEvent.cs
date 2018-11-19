namespace SmartDose.WcfClient.Services
{
    public enum ServiceNotifyEvent
    {
        ClientOpening,
        ClientOpened,
        ClientFaulted,
        ClientClosing,
        ClientClosed,

        ServiceErrorNotConnected,
        ServiceErrorAssemblyNotLoaded,

        ServiceStart,
        ServiceRunning,
        ServiceStop,
        ServiceDispose,
    }
}
