using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Inventory
#else
namespace SmartDose.RestDomain.Models.V2.Inventory
#endif
{
    /// <summary>
    /// The medicine stock.
    /// </summary>
    /// <seealso cref="SmartDose.Inventory.RESTv2.Models.Common.BaseData" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
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
        public override string ToString()
            => $"{MedicineCode}";
    }
}
