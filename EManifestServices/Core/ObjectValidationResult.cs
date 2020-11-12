using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class ObjectValidationResult
    {
        public bool IsValid { get; set; }
        public VoyageObjectErrorValidation ErrorValidation { get; set; }
    }
}