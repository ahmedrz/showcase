using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class ContainerObjectValidator : IObjectValidator<ContainerDetails>
    {
        public ObjectValidationResult Validate(ContainerDetails objectToValidate, VoyageValidationContext context)
        {
            ObjectValidationResult result = new ObjectValidationResult();
            result.IsValid = true;
            VoyageObjectErrorValidation validation = new VoyageObjectErrorValidation();
            validation.Entity = objectToValidate;
            validation.EntityType = objectToValidate.GetType().Name;
            result.ErrorValidation = validation;
            if (!string.IsNullOrEmpty(objectToValidate.ISOCode) && !context.DbContainerCodes.Any(l => l.IsoTypeCode == objectToValidate.ISOCode))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "ISOCode",
                    ErrorMessage = $"ISOCode value {objectToValidate.ISOCode} is not recognized."
                });
            }
            return result;
        }
    }
}