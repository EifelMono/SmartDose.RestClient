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
        ServiceRunning,
        ServiceStop,
        ServiceDispose,

        Error,
    }
}
