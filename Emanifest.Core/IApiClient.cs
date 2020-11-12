using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IApiClient
    {
        Guid ApiClientId { get; set; }

        string ApiClientName { get; set; }

        string ApiClientPassword { get; set; }

        string Role { get; set; }
        bool? IsActive { get; set; }
        Guid? CarrierId { get; set; }
    }
}
