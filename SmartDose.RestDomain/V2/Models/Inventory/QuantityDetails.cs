using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Inventory
{
    /// <summary>
    /// The quantity details.
    /// </summary>
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