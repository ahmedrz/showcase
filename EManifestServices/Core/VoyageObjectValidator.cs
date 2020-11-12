using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class VoyageObjectValidator : IObjectValidator<VoyageDetails>
    {
        public ObjectValidationResult Validate(VoyageDetails objectToValidate, VoyageValidationContext context)
        {
            ObjectValidationResult result = new ObjectValidationResult();
            result.IsValid = true;
            VoyageObjectErrorValidation validation = new VoyageObjectErrorValidation();
            validation.Entity = objectToValidate;
            validation.EntityType = objectToValidate.GetType().Name;
            result.ErrorValidation = validation;
            if (!string.IsNullOrEmpty(objectToValidate.PortCodeOfDischarge) && !context.DbLocations.Any(l => l.FullCode == objectToValidate.PortCodeOfDischarge))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "PortCodeOfDischarge",
                    ErrorMessage = $"PortCodeOfDischarge value {objectToValidate.PortCodeOfDischarge} is not recognized."
                });
            }
            return result;
        }
    }
}