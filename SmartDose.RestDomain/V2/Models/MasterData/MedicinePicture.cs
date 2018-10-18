using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Models.MasterData
{
    /// <summary>
    /// Medicine picture model
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MedicinePicture
    {
        /// <summary>
        /// Gets or sets the picture code.
        /// </summary>
        /// <value>
        /// The picture code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier for the picture is required.")]
        public string PictureCode { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Pictures of the medicine to be displayed. (e.g. Tray Filling)
        /// </summary>
        /// <value>
        /// The picture.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The picture data in Base64 encoding is required.")]
        public string PictureData { get; set; }

        /// <summary>
        /// Gets or sets the type of the picture.
        /// </summary>
        /// <value>
        /// The type of the picture.
        /// </value>
        [SpecificValuesValidation("Picture type is required.", @"image/jpeg", @"image/png")]
        public string PictureType { get; set; }
    }
}
