using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace EManifestServices.Attributes
{
    public class SecureAccessAttribute : EnableQueryAttribute
    {

        public override void ValidateQuery(HttpRequestMessage request, ODataQueryOptions queryOptions)
        {
            if (queryOptions.SelectExpand != null
                && queryOptions.SelectExpand.RawExpand != null
                && (queryOptions.SelectExpand.RawExpand.Contains("Users")))// || queryOptions.SelectExpand.RawExpand.Contains("Carriers")|| queryOptions.SelectExpand.RawExpand.Contains("BOLDetails"))
            {
                //Check here if user is allowed to view orders.
                throw new InvalidOperationException();
            }
            if (queryOptions.SelectExpand != null)
                queryOptions.SelectExpand.LevelsMaxLiteralExpansionDepth = 5;

            base.ValidateQuery(request, queryOptions);
        }
    }
}