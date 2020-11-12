using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface ILocationCode
    {
        int LocationCodeId { get; set; }

        string CountryCode { get; set; }

        string LocationCode { get; set; }


        string LocationName { get; set; }

        string Function { get; set; }

        int Serial { get; set; }
    }
}
