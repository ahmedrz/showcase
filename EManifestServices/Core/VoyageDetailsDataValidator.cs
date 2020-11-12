using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class VoyageDetailsDataValidator
    {


        VoyageValidationContext context;
        public VoyageDetailsDataValidator()
        {
        }

        public bool ValidateVoyageData(VoyageDetails voyage)
        {
            context = VoyageValidationContext.CreateContext(voyage);
            VoyageValidationException exception = new VoyageValidationException();
            bool valid = true;
            var voyageValidationResult = ValidateObject(voyage);
            if (voyageValidationResult?.IsValid == false)
            {
                valid = false;
                exception.ValidationErrors.Add(voyageValidationResult.ErrorValidation);
            };
            foreach (var bol in voyage.BOLDetails)
            {
                var bolValidationResult = ValidateObject(bol);
                if (bolValidationResult?.IsValid == false)
                {
                    valid = false;
                    exception.ValidationErrors.Add(bolValidationResult.ErrorValidation);
                };
                foreach (var container in bol.ContainerDetails)
                {
                    var containerValidationResult = ValidateObject(container);
                    if (containerValidationResult?.IsValid == false)
                    {
                        valid = false;
                        exception.ValidationErrors.Add(containerValidationResult.ErrorValidation);
                    };
                    foreach (var consignment in container.ConsignmentDetails)
                    {
                        var consignmentValidationResult = ValidateObject(consignment);
                        if (consignmentValidationResult?.IsValid == false)
                        {
                            valid = false;
                            exception.ValidationErrors.Add(consignmentValidationResult.ErrorValidation);
                        };
                    }
                }
                foreach (var consignment in bol.ConsignmentDetails)
                {
                    var consignmentValidationResult = ValidateObject(consignment);
                    if (consignmentValidationResult?.IsValid == false)
                    {
                        valid = false;
                        exception.ValidationErrors.Add(consignmentValidationResult.ErrorValidation);
                    };
                }
            }
            if (valid)
            {
                return true;
            }
            throw exception;

        }

        private ObjectValidationResult ValidateObject(object objectToValidate)
        {
            if (objectToValidate is VoyageDetails)
            {
                var validator = new VoyageObjectValidator();
                return validator.Validate(objectToValidate as VoyageDetails, context);
            }
            else if (objectToValidate is BOLDetails)
            {
                var validator = new BolObjectValidator();
                return validator.Validate(objectToValidate as BOLDetails, context);
            }
            else if (objectToValidate is ContainerDetails)
            {
                var validator = new ContainerObjectValidator();
                return validator.Validate(objectToValidate as ContainerDetails, context);
            }
            else if (objectToValidate is ConsignmentDetails)
            {
                var validator = new ConsignmentObjectValidator();
                return validator.Validate(objectToValidate as ConsignmentDetails, context);
            }
            else
            {
                return null;
            }
        }
    }
}