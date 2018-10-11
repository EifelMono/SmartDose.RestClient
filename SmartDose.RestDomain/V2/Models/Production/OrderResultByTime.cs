using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Order result by time
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTv2.Model.BaseData" />    
    public class OrderResultByTime
    {
        /// <summary>
        /// Gets or sets the unique identifier of the external order.
        /// </summary>
        /// <value>
        /// The order code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the order is required.")]
        public string OrderCode { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the machine which executes the order
        /// </summary>
        /// <value>
        /// The machine code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Machine code is required.")]
        public string MachineCode { get; set; }

        /// <summary>
        /// Gets or sets the dispense state.
        /// The progress of the production
        /// </summary>
        /// <value>
        /// The state of the dispense.
        /// </value>
        [EnumValidation(typeof(DispenseStatus))]
        public string DispenseState { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ssZ", "Creation Date is required with yyyy-MM-ddTHH:mm:ssZ format.")]
        public string CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the production date.
        /// </summary>
        /// <value>
        /// The production date.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ssZ", "Creation Date is required with yyyy-MM-ddTHH:mm:ssZ format.")]
        public string ProductionDate { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Order details are required.")]
        public List<ResultOrderDetail> ResultOrderDetails { get; set; }
    }
}
