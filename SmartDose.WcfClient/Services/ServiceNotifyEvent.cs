namespace SmartDose.WcfClient.Services
{
    public enum ServiceNotifyEvent
    {
        ClientOpening,
        ClientOpened,
        ClientFaulted,
        ClientClosing,
        ClientClosed,

        ServiceStart,
        ServiceStop,
        ServiceDispose,
    }
}
