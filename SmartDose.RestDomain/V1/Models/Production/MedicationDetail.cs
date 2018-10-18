﻿using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MedicationDetail
    {
        public MedicationDetail()
        {
        }

        /// <summary>
        /// Gets or sets the medicine id.
        /// </summary>
        public string MedicineId { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public float Count { get; set; }

        /// <summary>
        /// Gets or sets the intake advice.
        /// </summary>
        public string IntakeAdvice { get; set; }

        /// <summary>
        /// Gets or sets the physician.
        /// </summary>
        public string Physician { get; set; }

        /// <summary>
        /// Gets or sets the physician comment.
        /// </summary>
        public string PhysicianComment { get; set; }

        /// <summary>
        /// Gets or sets the medication name that should be printed on the pouch.
        /// This is only valid for this order.
        /// </summary>
        public string PrescribedMedicine { get; set; }
    }
}
