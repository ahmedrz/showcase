using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace EManifestServices.FilesDownloadCore
{
    public class FileDownloadResult : ActionResult
    {
        private HttpResponseMessage responseMessage;

        public FileDownloadResult(HttpResponseMessage webApiResponse)
        {
            responseMessage = webApiResponse;
        }

        /// <summary>
        /// The below method will respond with the Video file
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            //The header information
            context.HttpContext.Response.StatusCode = (int)responseMessage.StatusCode;
            context.HttpContext.Response.AddHeader("Content-Disposition", responseMessage.Content.Headers.ContentDisposition.ToString());
            context.HttpContext.Response.AddHeader("Accept-Ranges", responseMessage.Headers.AcceptRanges.ToString());
            context.HttpContext.Response.AddHeader("Content-Type", responseMessage.Content.Headers.ContentType.ToString());
            context.HttpContext.Response.AddHeader("Content-Length", responseMessage.Content.Headers.ContentLength.ToString());
            if (responseMessage.Content.Headers.ContentRange!=null)
            {
                 context.HttpContext.Response.AddHeader("Content-Range", responseMessage.Content.Headers.ContentRange.ToString());
            }           
            //content
            var binaryData = responseMessage.Content.ReadAsByteArrayAsync().Result;
            context.HttpContext.Response.BinaryWrite(binaryData);

        }

       
    }
}