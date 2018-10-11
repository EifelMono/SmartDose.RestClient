using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Represent the details of one medicine to be dispenced.
    /// </summary>
    public class ResultMedicationDetail 
    {

        /// <summary>
        /// Gets or sets the unique medicine code.
        /// </summary>
        /// <value>
        /// The medicine code.
        /// </value>
        [Required(ErrorMessage = "MedicineCode is required")]
        public string MedicineCode { get; set; }

        /// <summary>
        /// Gets or sets the quantity to be dispensed.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [Required(ErrorMessage = "Quantity is required")]
        public double Quantity { get; set; }

    }
}
