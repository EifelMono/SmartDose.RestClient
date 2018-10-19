using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SmartDose.RestDomain.Validation
{
    /// <summary>
    /// DateTime validation attribute
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class CultureValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeValidationAttribute"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public CultureValidationAttribute(string errorMessage): base(errorMessage)
        {
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
            try
            {
                var culture = CultureInfo.GetCultureInfo(value as string);
                return true;
            }
            catch 
            {
            }
            return false;

        }
    }
}