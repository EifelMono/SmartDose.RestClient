using System.ComponentModel;

namespace SmartDose.RestDomain.V2.Model.MasterData
{
    /// <summary>
    /// Medicine status
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The released
        /// </summary>
        [Description("Released")]
        Released = 0,

        /// <summary>
        /// The created
        /// </summary>
        [Description("Created")]
        Created = 1,

        /// <summary>
        /// The quarantine
        /// </summary>
        [Description("Quarantine")]
        Quarantine = 2
    }

    /// <summary>
    /// Filling mode
    /// </summary>
    public enum FillingMode
    {
        /// <summary>
        /// The multidose
        /// </summary>
        [Description("Multidose")]
        Multidose = 0,

        /// <summary>
        /// The unit dose
        /// </summary>
        [Description("UnitDose")]
        UnitDose = 1,

        /// <summary>
        /// The combi dose
        /// </summary>
        [Description("CombiDose")]
        CombiDose = 2
    }
}
