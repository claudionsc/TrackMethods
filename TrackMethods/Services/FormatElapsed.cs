using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMethods.Interfaces;

namespace TrackMethods.Services
{
    internal static class FormatElapsed
    {
        public static string ElapsedFormatter(TimeSpan ts)
        {
            return string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }
    }
}
