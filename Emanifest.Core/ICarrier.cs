using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface ICarrier
    {
        Guid CarrierId { get; set; }
        string CarrierName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        bool? IsActive { get; set; }
        string Info { get; set; }
    }
}
