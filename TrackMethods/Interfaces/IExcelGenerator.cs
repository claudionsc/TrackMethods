using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMethods.Interfaces
{
    internal interface IExcelGenerator
    {
        void GenerateExcel(Stopwatch sw, string methodCalled, string description, string user, string saveFilePath, string fileName, Int32 milisseconds);

    }
}
