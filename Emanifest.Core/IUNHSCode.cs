using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IUNHSCode
    {
        int UNHSCodeId { get; set; }

        string Classification { get; set; }

        string Code { get; set; }

        string Description { get; set; }

        string ParentCode { get; set; }

        int Level { get; set; }

        bool IsLeaf { get; set; }

        int Serial { get; set; }
    }
}
