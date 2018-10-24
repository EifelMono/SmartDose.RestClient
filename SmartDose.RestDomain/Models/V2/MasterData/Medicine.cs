using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SmartDose.RestDomain.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.MasterData
#else
namespace SmartDose.RestDomain.Models.V2.MasterData
#endif
{
    /// <summary>
    /// Medicine model
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class Medicine
    {
        /// <summary>
        /// Gets or sets the medicine code.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The unique identifier for the medicine is required.")]
        public string MedicineCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the medicine.
        /// </summary>
        /// <value>
        /// The name of the medicine.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name of the medicine is required.")]
        public string MedicineName { get; set; }

        /// <summary>
        /// Gets or sets the generic name.
        /// </summary>
        public string GenericName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [EnumValidation(typeof(Status), optional: true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the additional medicine codes.
        /// </summary>
        public AdditionalMedicineCode[] AdditionalMedicineCodes { get; set; }

        /// <summary>
        /// Gets or sets the print details.
        /// </summary>
        /// <value>
        /// The print details.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Array of print details for the medicine are required.")]
        public MedicinePrintDetail[] PrintDetails { get; set; }

        /// <summary>
        /// Gets or sets the medicine pictures.
        /// </summary>
        /// <value>
        /// The medicine pictures.
        /// </value>
        public MedicinePicture[] MedicinePictures { get; set; }

        /// <summary>
        /// Gets or sets the production attributes.
        /// </summary>
        public ProductionAttributes ProductionAttributes { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer code.
        /// </summary>
        /// <value>
        /// The manufacturer code.
        /// </value>
        public string ManufacturerCode { get; set; }

        public override string ToString()
            => $"{MedicineCode} {MedicineName}";
    }
}
