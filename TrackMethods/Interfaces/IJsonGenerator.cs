using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TrackMethods.Interfaces
{
    internal interface IJsonGenerator
    {
        JsonResult GenerateJson(Stopwatch sw, string methodCalled, string description, string user, string fileName, Int32 milisseconds);
    }
}
