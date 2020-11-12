using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EManifestServices.Helpers
{
    public class JsonNetResult : ActionResult
    {
        private const string _dateFormat = "yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ";

        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }
        public JsonNetResult(object data) : this()
        {
            Data = data;
        }

        public JsonSerializerSettings SerializerSettings { get; set; }
        public Formatting Formatting { get; set; }

        public JsonNetResult()
        {
            SerializerSettings = new JsonSerializerSettings();
            Formatting = Formatting.Indented;
            SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            //SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType)
              ? ContentType
              : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                var isoConvert = new IsoDateTimeConverter();
                isoConvert.DateTimeFormat = _dateFormat;
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

                JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
                serializer.Converters.Add(isoConvert);

                serializer.Serialize(writer, Data);

                writer.Flush();
            }
        }
    }
}