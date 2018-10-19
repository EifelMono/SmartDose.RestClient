using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Inventory
#else
namespace SmartDose.RestDomain.Models.V2.Inventory
#endif
{
    /// <summary>
    /// The quantity details.
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class QuantityDetails 
    {
        /// <summary>
        /// Gets or sets the bulk packages.
        /// </summary>
        /// <value>
        /// The bulk packages.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Value for Bulk Packages is required.")]
        public double BulkPackages { get; set; }

        /// <summary>
        /// Gets or sets the stock bottles.
        /// </summary>
        /// <value>
        /// The stock bottles.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Value for Stock Bottles is required.")]
        public double StockBottles { get; set; }

        /// <summary>
        /// Gets or sets the canisters.
        /// </summary>
        /// <value>
        /// The canisters.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Value for Canisters is required.")]
        public double Canisters { get; set; }
    }
}
