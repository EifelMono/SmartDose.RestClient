namespace SmartDose.RestDomain.V1.Models
{
    public enum DispenseState
    {
        DispenseInit,
        DispenseStarted,
        DispenseFinished,
        DispenseCanceled,
        OrderNotYetImported,
        OrderNotFound
    }
}
