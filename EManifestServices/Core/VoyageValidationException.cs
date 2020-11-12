using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class VoyageValidationException : Exception
    {
        public VoyageValidationException()
        {
            ValidationErrors = new List<VoyageObjectErrorValidation>();
        }
        public List<VoyageObjectErrorValidation> ValidationErrors { get; set; }

    }

    public class VoyageObjectErrorValidation
    {
        public VoyageObjectErrorValidation()
        {
            ErrorMessages = new List<VoyageObjectErrorMessage>();
        }
        public object Entity { get; set; }
        public string EntityType { get; set; }

        public List<VoyageObjectErrorMessage> ErrorMessages { get; set; }
    }
    public class VoyageObjectErrorMessage
    {
        public string ErrorMessage { get; set; }
        public string Property { get; set; }

    }
}