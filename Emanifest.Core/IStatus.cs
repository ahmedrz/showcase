using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IStatus
    {
        Guid StatusId { get; set; }
        string StatusDesc { get; set; }
        bool? AllowModify { get; set; }
    }
}
