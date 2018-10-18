namespace SmartDose.RestDomain.V1.Models
{
    public enum OrderState
    {
        Undefined,
        ValidationOk,
        ValidationFailed,
        ReadyForProduction,
        InQueue,
        ProductionFinished,
        ProductionCancelled,
        InProduction,
        ReValidationRequested,
        NewRepairOrder
    }
}
