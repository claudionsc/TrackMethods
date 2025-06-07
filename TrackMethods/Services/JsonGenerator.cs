using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrackMethods.Interfaces;

namespace TrackMethods.Services
{
    internal class JsonGenerator : IJsonGenerator
    {
        public JsonResult GenerateJson(Stopwatch sw, string methodCalled, string description, string user, string fileName, Int32 milisseconds)
        {
            string elapsedTime = FormatElapsed.ElapsedFormatter(sw.Elapsed);
            var json = new JsonResult
            {
                Data = new
                {
                    timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"),
                    elapsedTime,
                    methodCalled,
                    description,
                    user,
                    fileName,
                    milisseconds
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            Console.WriteLine("JSON gerado com sucesso");

            return json;
        }
    }
}
