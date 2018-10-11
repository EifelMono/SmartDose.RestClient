﻿using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Multidose Order Model
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTv2.Model.BaseData" />
    public class Order
    {
        /// <summary>
        /// Gets or sets the order code.
        /// </summary>
        /// <value>
        /// The order code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the order is required")]
        public string OrderCode { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ssZ", "Timestamp is required with yyyy-MM-ddTHH:mm:ssZ format.")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        [Required(ErrorMessage = "Order Details are required")]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}