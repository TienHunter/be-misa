using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidDateLessNow : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Perform your custom validation logic here
            // If the validation fails, return a ValidationResult with the error message

            // Example: Check if the value is null or empty
            if (value != null && !string.IsNullOrEmpty(value.ToString()) && (DateTime)value > DateTime.Now)
            {
                // validate ngay sinh khong lon hon ngay hien tai

                return new ValidationResult(ErrorMessage);

            }
            // Custom validation logic here...

            return ValidationResult.Success;
        }
    }
}
