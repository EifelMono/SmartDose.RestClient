using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace SmartDose.RestDomain.Validation
{
    /// <summary>
    /// DateTime validation attribute
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class SpecificValuesValidationAttribute : ValidationAttribute
    {
        private List<string> _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeValidationAttribute"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public SpecificValuesValidationAttribute(string errorMessage, params string[] values) : base(errorMessage)
        {
            _values = values.ToList();
        }
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified value is valid; otherwise, <see langword="false" />.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (_values == null) return true;
            return _values.Contains(value as string);
        }
    }
}