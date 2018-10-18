using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Models.Production
{
    /// <summary>
    /// Order result.
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
    /// <seealso cref="SmartDose.Production.RESTv2.Model" />
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class OrderResult
    {
        /// <summary>
        /// Gets or sets the order code.
        /// </summary>
        /// <value>
        /// The order code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the external order is required.")]
        public string OrderCode { get; set; }

        /// <summary>
        /// Gets or sets the machine code.
        /// </summary>
        /// <value>
        /// The machine code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identifier of the machine which executes the Order is required.")]
        public string MachineCode { get; set; }

        /// <summary>
        /// Gets or sets the state of the dispense.
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
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ssZ", "Production Date is required with yyyy-MM-ddTHH:mm:ssZ format.")]
        public string ProductionDate { get; set; }

        /// <summary>
        /// Gets or sets the pouches.
        /// </summary>
        /// <value>
        /// The pouches.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "List of pouches is required.")]
        public Pouch[] Pouches { get; set; }
    }
}
