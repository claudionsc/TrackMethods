using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace TrackMethods.Interfaces
{
    public interface ITrackMethod
    {
        T TrackExecution<T>(Func<T> func, string description, string methodCalled, string user, string? filePath, string fileName, string saveAs, int milisseconds);
        string LastGeneratedJson { get; }
    }
}
