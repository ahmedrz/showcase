using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EManifestServices.DAL
{
    class SpecificStringAttribute : ValidationAttribute
    {
        public string[] AllowedStrings { get; set; }
        public string Info { get; set; }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !AllowedStrings.Any() || AllowedStrings.ToList().Contains((string)value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"Value is invalid. Allowed values are ({string.Join(",", AllowedStrings)}) , Info : {Info}");
            }
        }
    }
}
