using Newtonsoft.Json;
using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SmartDose.RestDomain.Converter;

#if RestDomainDev
using SmartDose.RestDomainDev.PropertyEditorThings;
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
#if RestDomainDev
        [CategoryAsString]
#endif
        [EnumValidation(typeof(Status), optional: true)]
        public string Status { get; set; }
#if RestDomainDev
        [CategoryAsType]
#endif
        [JsonIgnore]
        public Status StatusAsType
        {
            get => NameAsTypeConverter.StringToEnum<Status>(Status);
            set => Status = NameAsTypeConverter.EnumToString(value);
        }

        /// <summary>
        /// Gets or sets the additional medicine codes.
        /// </summary>
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif 
        public List<AdditionalMedicineCode> AdditionalMedicineCodes { get; set; } = new List<AdditionalMedicineCode>();

        /// <summary>
        /// Gets or sets the print details.
        /// </summary>
        /// <value>
        /// The print details.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Array of print details for the medicine are required.")]
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif 
        public List<MedicinePrintDetail> PrintDetails { get; set; } = new List<MedicinePrintDetail>();

        /// <summary>
        /// Gets or sets the medicine pictures.
        /// </summary>
        /// <value>
        /// The medicine pictures.
        /// </value>
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<MedicinePicture> MedicinePictures { get; set; } = new List<MedicinePicture>();

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
