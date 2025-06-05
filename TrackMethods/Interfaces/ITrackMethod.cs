using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace TrackMethods.Interfaces
{
    public interface ITrackMethod
    {
        T TrackExecution<T>(Func<T> func, string description, string methodCalled, string user, string? filePath, string fileName, string saveAs, int milisseconds);
        void SaveExcel(Stopwatch sw, string methodCalled, string description, string user, string saveFilePath, string fileName, int milisseconds);
        JsonResult GenerateJson(Stopwatch sw, string methodCalled, string description, string user, string fileName, int milisseconds);
        string LastGeneratedJson { get; }
    }
}
