using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EManifestServices.Core
{
    interface IObjectValidator<T> where T : class
    {
        ObjectValidationResult Validate(T objectToValidate,VoyageValidationContext context);
    }
}
