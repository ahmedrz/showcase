using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EManifestServices.Helpers
{
    public class CheckChildrenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GeniusDMValidationResult result = new GeniusDMValidationResult();
            result.ErrorMessage = string.Format(@"Error occured at {0}", validationContext.DisplayName);

            IEnumerable list = value as IEnumerable;
            if (list == null)
            {
                // Single Object

                List<ValidationResult> results = new List<ValidationResult>();
                ValidationContext context = new ValidationContext(value, validationContext.ServiceContainer, null);
                var isValid = Validator.TryValidateObject(value, context, results, true);
                if (!isValid)
                {
                    result.NestedResults = results;
                    return result;
                }
                return null;
            }
            else
            {
                List<ValidationResult> recursiveResultList = new List<ValidationResult>();
                // List Object
                foreach (var item in list)
                {
                    List<ValidationResult> nestedItemResult = new List<ValidationResult>();
                    ValidationContext context = new ValidationContext(item, validationContext.ServiceContainer, null);

                    GeniusDMValidationResult nestedParentResult = new GeniusDMValidationResult();
                    nestedParentResult.ErrorMessage = string.Format(@"Error occured at {0}", validationContext.DisplayName);

                    var isValid = Validator.TryValidateObject(item, context, nestedItemResult, true);
                    if (!isValid)
                    {
                        nestedParentResult.NestedResults = nestedItemResult;
                        recursiveResultList.Add(nestedParentResult);
                    }

                }
                if (recursiveResultList.Count > 0)
                {
                    result.NestedResults = recursiveResultList;
                    return result;
                }
                return null;

            }
        }
    }
    public class GeniusDMValidationResult : ValidationResult
    {
        public GeniusDMValidationResult() : base("")
        {

        }
        public IList<ValidationResult> NestedResults { get; set; }
    }
}