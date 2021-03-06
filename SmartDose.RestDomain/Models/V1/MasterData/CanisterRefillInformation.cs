﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.MasterData
#else
namespace SmartDose.RestDomain.Models.V1.MasterData
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class CanisterRefillInformation
    {
        /// <summary>
        /// Gets or sets the lot nr.
        /// </summary>
        [Required(ErrorMessage = "LotNr is required")]
        public string LotNr { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        [Required(ErrorMessage = "ExpirationDate is required")]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether canister empty.
        /// </summary>
        [Required(ErrorMessage = "CanisterEmpty is required")]
        public bool CanisterEmpty { get; set; }
    }
}
