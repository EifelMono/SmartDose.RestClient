﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Models.Production
{
    /// <summary>
    /// Destination Facility Model
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.ContactEx" />
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DestinationFacility : ContactEx
    {
        /// <summary>
        /// Gets or sets the destination facility code.
        /// </summary>
        /// <value>
        /// The destination facility code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "DestinationFacilityCode is required")]
        public string DestinationFacilityCode { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name line.
        /// </summary>
        /// <value>
        /// The name line.
        /// </value>
        public string NameLine { get; set; }

        /// <summary>
        /// Gets or sets the department code.
        /// </summary>
        /// <value>
        /// The department code.
        /// </value>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        /// <value>
        /// The name of the department.
        /// </value>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the ward code.
        /// </summary>
        /// <value>
        /// The ward code.
        /// </value>
        public string WardCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the ward.
        /// </summary>
        /// <value>
        /// The name of the ward.
        /// </value>
        public string WardName { get; set; }

        /// <summary>
        /// Gets or sets the room number.
        /// </summary>
        /// <value>
        /// The room number.
        /// </value>
        public string RoomNumber { get; set; }

        /// <summary>
        /// Gets or sets the bed number.
        /// </summary>
        /// <value>
        /// The bed number.
        /// </value>
        public string BedNumber { get; set; }
    }
}
