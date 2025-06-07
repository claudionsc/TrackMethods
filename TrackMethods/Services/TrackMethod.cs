using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using TrackMethods.Interfaces;

namespace TrackMethods.Services
{
    public class TrackMethod : ITrackMethod
    {
        private readonly ExcelGenerator _excelGenerator = new();
        private readonly JsonGenerator _jsonGenerator = new();

        public string LastGeneratedJson { get; private set; } = string.Empty;
        public T TrackExecution<T>(
            Func<T> func,
            string description,
            string methodCalled,
            string user,
            string? filePath,
            string fileName,
            string saveAs,
            Int32 milisseconds
        )
        {
            var saveType = saveAs.ToLower();

            var stopwatch = Stopwatch.StartNew();
            T result;

            try
            {
                result = func();
                if (func == null)
                    throw new Exception("Método inválido");

                if (saveType == "xlsx" || saveType == "excel")
                {
                    if (filePath == null)
                        throw new Exception("Caminho obrigatório para Excel");

                    _excelGenerator.GenerateExcel(stopwatch, methodCalled, description, user, filePath, fileName, milisseconds);
                }
                else if (saveType == "json")
                {
                    if (filePath != null)
                        throw new Exception("JSON não deve usar filePath");

                    var logData = _jsonGenerator.GenerateJson(stopwatch, methodCalled, description, user, fileName, milisseconds);
                    LastGeneratedJson = JsonConvert.SerializeObject(logData, Formatting.Indented);
                }
                else
                {
                    throw new Exception("Formato inválido. Use json ou xlsx.");
                }

                return result;
            }
            finally
            {
                stopwatch.Stop();
            }

        }
    }
}
