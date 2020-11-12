using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IPackageCode
    {
        int PackageCodeId { get; set; }

        string PackageCode { get; set; }

        string PackageDescription { get; set; }

        int Serial { get; set; }
    }
}
