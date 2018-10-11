using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Inventory
{
    /// <summary>
    /// The medicine stock.
    /// </summary>
    /// <seealso cref="SmartDose.Inventory.RESTv2.Models.Common.BaseData" />
    public class MedicineStock 
    {
        /// <summary>
        /// Unique medicine code
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier for medicine is required.")]
        public string MedicineCode { get; set; }

        /// <summary>
        /// The aggregated quantity of the medicine in the system 
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity is required.")]
        public double Quantity { get; set; }

        /// <summary>
        /// Gives the details for the different entities and the corresponding quantity of stock in the system
        /// </summary>
        public QuantityDetails QuantityDetails { get; set; }
    }
}