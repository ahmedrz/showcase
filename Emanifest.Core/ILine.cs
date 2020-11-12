using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface ILine
    {
        string LineCode { get; set; }

        string LineName { get; set; }

        int Serial { get; set; }
        bool? IsActive { get; set; }
    }
}
