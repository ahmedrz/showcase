using EManifestServices.DAL;
using System.Linq;

namespace EManifestServices.Core
{
    public class ConsignmentObjectValidator : IObjectValidator<ConsignmentDetails>
    {
        public ObjectValidationResult Validate(ConsignmentDetails objectToValidate, VoyageValidationContext context)
        {
            ObjectValidationResult result = new ObjectValidationResult();
            result.IsValid = true;
            VoyageObjectErrorValidation validation = new VoyageObjectErrorValidation();
            validation.Entity = objectToValidate;
            validation.EntityType = objectToValidate.GetType().Name;
            result.ErrorValidation = validation;
            if (!string.IsNullOrEmpty(objectToValidate.CommodityCode) && !context.DbHsCodes.Any(l => l.Code == objectToValidate.CommodityCode))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "CommodityCode",
                    ErrorMessage = $"CommodityCode value {objectToValidate.CommodityCode} is not recognized."
                });
            }
            if (!string.IsNullOrEmpty(objectToValidate.PackageTypeCode) && !context.DbPackageCodes.Any(l => l.PackageCode == objectToValidate.PackageTypeCode))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "PackageTypeCode",
                    ErrorMessage = $"PackageTypeCode value {objectToValidate.PackageTypeCode} is not recognized."
                });
            }
            if (!string.IsNullOrEmpty(objectToValidate.UnNumberOfDangerousGoods) && !context.DbHazardousCodes.Any(l => l.Code == objectToValidate.UnNumberOfDangerousGoods))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "UnNumberOfDangerousGoods",
                    ErrorMessage = $"UnNumberOfDangerousGoods value {objectToValidate.UnNumberOfDangerousGoods} is not recognized."
                });
            }
            return result;
        }
    }
}