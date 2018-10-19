using SmartDose.RestDomain.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Inventory
#else
namespace SmartDose.RestDomain.Models.V2.Inventory
#endif
{
    /// <summary>
    /// The stock bottle.
    /// </summary>
    /// <seealso cref="SmartDose.Inventory.RESTv2.Models.Common.BaseData" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class StockBottle
    {
        /// <summary>
        /// Unique identifier for the stock bottle
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier for the stock bottle is required.")]
        public string StockBottleCode { get; set; }

        /// <summary>
        /// Unique medicine code of the stock bottle
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Medicine code is required.")]
        public string MedicineCode { get; set; }

        /// <summary>
        /// Quantity in the stock bottle
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity is required.")]
        public double Quantity { get; set; }

        /// <summary>
        /// Expiry date of the stock bottle
        /// </summary>
        [DateTimeValidation("yyyy-MM-dd", "ExpiryDate is required with yyyy-MM-dd format.", optional:true)]
        public string ExpiryDate { get; set; }

        /// <summary>
        /// Batch number of the stock bottle
        /// </summary>
        public string BatchNumber { get; set; }
    }
}
