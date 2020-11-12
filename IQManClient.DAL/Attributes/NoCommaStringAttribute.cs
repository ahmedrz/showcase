using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQManClient.DAL
{
    class NoCommaStringAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value.ToString().Contains(","))
            {
                return new ValidationResult($"Comma (,) cannot be used in fields , property={validationContext.MemberName}");
            }
            return ValidationResult.Success;

        }
    }
}

