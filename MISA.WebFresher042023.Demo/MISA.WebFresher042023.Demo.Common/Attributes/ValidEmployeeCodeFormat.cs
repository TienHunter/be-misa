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
    public class ValidEmployeeCodeFormat : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Perform your custom validation logic here
            // If the validation fails, return a ValidationResult with the error message

            // Example: Check if the value is null or empty
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                string patten = @"^(NV-)(\d+)$";
                
                if (!Regex.IsMatch(value.ToString(), patten))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            // Custom validation logic here...

            return ValidationResult.Success;
        }
    }
}
