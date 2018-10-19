using System;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.Validation
{
    /// <summary>
    /// Enum validation attribute
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class EnumValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// The type
        /// </summary>
        private Type _type;
        private readonly bool _optional;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumValidationAttribute" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public EnumValidationAttribute(Type type, bool optional = false) 
        {
            ErrorMessage=$"{type.Name} is incorrect, must either of: {string.Join(",", Enum.GetNames(type))}";
            _type = type;
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
            return _type.IsEnum && Enum.IsDefined(_type, value);
        }
    }
}
