using MISA.WebFresher042023.Demo.Common.Commons;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullOrEmpty : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Perform your custom validation logic here
            // If the validation fails, return a ValidationResult with the error message

            // Example: Check if the value is null or empty
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                //var errors = new List<string> { ErrorMessage };

                return new ValidationResult(ErrorMessage);
            }


            // Custom validation logic here...

            return ValidationResult.Success;
        }
    }
}