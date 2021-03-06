﻿using System;
using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.Inventory
#else
namespace SmartDose.RestDomain.Models.V1.Inventory
#endif
{
    /// <summary>
    /// The medicine container.
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class MedicineContainer
    {
        /// <summary>
        /// Gets or sets the unique id.
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>
        ///     Represents the original barcode of the container. Ex. Pharmacy code.
        /// </summary>
        public string OriginalBarcode { get; set; }

        /// <summary>
        /// Gets or sets the medicine id.
        /// </summary>
        public string MedicineId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        public DateTime EntryDate { get; set; } 

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the batch number.
        /// </summary>
        public string BatchNumber { get; set; }
    }
}
