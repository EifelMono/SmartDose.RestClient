namespace SmartDose.RestDomain.V2.Model.MasterData
/// <summary>
/// Pharmaceutical Attributes
/// </summary>
{
    public class PharmaceuticalAttributes
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is a narcotic.
        /// </summary>
        /// <value>
        ///   <c>true</c> if narcotic; otherwise, <c>false</c>.
        /// </value>
        public bool Narcotic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a cytostatic.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cytostatic; otherwise, <c>false</c>.
        /// </value>
        public bool Cytostatic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it needs cooling.
        /// </summary>
        /// <value>
        ///   <c>true</c> if needs cooling; otherwise, <c>false</c>.
        /// </value>
        public bool NeedsCooling { get; set; }
    }
}