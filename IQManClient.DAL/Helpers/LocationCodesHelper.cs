using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQManClient.DAL.Helpers
{
    public static class LocationCodesHelper
    {
        public static bool? IsPort(LocationCodes location)
        {
            return location?.Function?.StartsWith("1");
        }
    }

}
