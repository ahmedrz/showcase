using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Attributes
{
    public class RequiredIfAttribute : ValidationAttribute
    {

        RequiredAttribute _innerAttribute = new RequiredAttribute();
        private string _dependentProperty { get; set; }
        private string _targetValueAsString { get; set; }

        public RequiredIfAttribute(string dependentProperty, string targetValueAsString)
        {
            this._dependentProperty = dependentProperty;
            this._targetValueAsString = targetValueAsString;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                if ((dependentValue == null && _targetValueAsString == null) || (dependentValue.ToString().Equals(_targetValueAsString)))
                {
                    if (!_innerAttribute.IsValid(value))
                    {
                        string name = validationContext.DisplayName;
                        return new ValidationResult(ErrorMessage = name + " Is required.");
                    }
                }
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(_dependentProperty));
            }
        }

    }
}