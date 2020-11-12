using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface ICountryCode
    {
        int CountryCodeId { get; set; }

        string CountryCode { get; set; }

        string CountryName { get; set; }

        int Serial { get; set; }
    }
}
