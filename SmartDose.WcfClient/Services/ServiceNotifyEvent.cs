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
        ServiceErrorAssemblyBad,

        ServiceInited,
        ServiceStart,
        ServiceStarted,
        ServiceRunning,
        ServiceStop,
        ServiceDispose,

        Error,
    }
}
