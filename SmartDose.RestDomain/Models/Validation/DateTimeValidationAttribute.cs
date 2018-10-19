using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SmartDose.RestDomain.Validation
{
    /// <summary>
    /// DateTime validation attribute
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class DateTimeValidationAttribute: ValidationAttribute
    {
        /// <summary>
        /// The format
        /// </summary>
        private readonly string _format;
        private readonly bool _optional;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeValidationAttribute"/> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="errorMessage">The error message.</param>
        public DateTimeValidationAttribute(string format, string errorMessage, bool optional = false): base(errorMessage)
        {
            _format = format;
            _optional = optional;
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
            if (_optional && string.IsNullOrEmpty(value as string))
                return true;
            return DateTime.TryParseExact(value as string, _format, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault, out _);
        }
    }
}
