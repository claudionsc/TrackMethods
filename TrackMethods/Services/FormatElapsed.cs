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
        public static string ElapsedFormatter(TimeSpan elapsed)
        {
            return elapsed.ToString(@"hh\:mm\:ss\.fff");
        }
    }
}
