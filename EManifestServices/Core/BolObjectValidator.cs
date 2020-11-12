using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class BolObjectValidator : IObjectValidator<BOLDetails>
    {
        public ObjectValidationResult Validate(BOLDetails objectToValidate, VoyageValidationContext context)
        {
            ObjectValidationResult result = new ObjectValidationResult();
            result.IsValid = true;
            VoyageObjectErrorValidation validation = new VoyageObjectErrorValidation();
            validation.Entity = objectToValidate;
            validation.EntityType = objectToValidate.GetType().Name;
            result.ErrorValidation = validation;
            if (!string.IsNullOrEmpty(objectToValidate.PortCodeOfOrigin) && !context.DbLocations.Any(l => l.FullCode == objectToValidate.PortCodeOfOrigin))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "PortCodeOfOrigin",
                    ErrorMessage = $"PortCodeOfOrigin value {objectToValidate.PortCodeOfOrigin} is not recognized."
                });
            }
            if (!string.IsNullOrEmpty(objectToValidate.PortCodeOfLoading) && !context.DbLocations.Any(l => l.FullCode == objectToValidate.PortCodeOfLoading))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "PortCodeOfLoading",
                    ErrorMessage = $"PortCodeOfLoading value {objectToValidate.PortCodeOfLoading} is not recognized."
                });
            }
            if (!string.IsNullOrEmpty(objectToValidate.PortCodeOfDischarge) && !context.DbLocations.Any(l => l.FullCode == objectToValidate.PortCodeOfDischarge))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "PortCodeOfDischarge",
                    ErrorMessage = $"PortCodeOfDischarge value {objectToValidate.PortCodeOfDischarge} is not recognized."
                });
            }
            if (!string.IsNullOrEmpty(objectToValidate.PortCodeOfDestination) && !context.DbLocations.Any(l => l.FullCode == objectToValidate.PortCodeOfDestination))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "PortCodeOfDestination",
                    ErrorMessage = $"PortCodeOfDestination value {objectToValidate.PortCodeOfDestination} is not recognized."
                });
            }

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


            if (!string.IsNullOrEmpty(objectToValidate.CountryOfOrigin) && !context.DbCountries.Any(l => l.CountryCode == objectToValidate.CountryOfOrigin))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "CountryOfOrigin",
                    ErrorMessage = $"CountryOfOrigin value {objectToValidate.CountryOfOrigin} is not recognized."
                });
            }

            if (!string.IsNullOrEmpty(objectToValidate.ShipperCountryCode) && !context.DbCountries.Any(l => l.CountryCode == objectToValidate.ShipperCountryCode))
            {
                result.IsValid = false;
                validation.ErrorMessages.Add(new VoyageObjectErrorMessage
                {
                    Property = "ShipperCountryCode",
                    ErrorMessage = $"ShipperCountryCode value {objectToValidate.ShipperCountryCode} is not recognized."
                });
            }
            return result;
        }
    }
}