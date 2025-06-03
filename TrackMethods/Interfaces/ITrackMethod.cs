    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    namespace TrackMethods.Interfaces
    {
        public interface ITrackMethod
        {
            T TrackExecution<T>(Func<T> func, string methodCalled, string description, string user, string? filePath, string filelame, string saveAs, Int32 milisseconds);
            void SaveExcel(Stopwatch sw, string methoGalled, string description, string user, string saveFilePath, string filelame, Int32 milisseconds);
            JsonResult GenerateJson(Stopwatch sw, string methodCalled, string description, string user, string fileName, Int32 milisseconds);
            string LastGeneratedJson { get; }

        }
    }
