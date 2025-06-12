using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using TrackMethods.Interfaces;
using TrackMethods.Services;


delegate void GenerateExcelHandler(
    Stopwatch sw,
    string methodCalled,
    string description,
    string user,
    string saveFilePath,
    string fileName,
    Int32 milisseconds
);

delegate JsonResult GenerateJsonHandler(
    Stopwatch sw, 
    string methodCalled, 
    string description, 
    string user, 
    string fileName, 
    Int32 milisseconds);

namespace TrackMethods.Services
{
    public class TrackMethod : ITrackMethod
    {

        private readonly ExcelGenerator _excelGenerator = new();
        private readonly JsonGenerator _jsonGenerator = new();

        event GenerateExcelHandler GenerateExcelEvent;
        event GenerateJsonHandler GenerateJsonEvent;

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

                    GenerateExcelEvent += _excelGenerator.GenerateExcel;

                    GenerateExcelEvent?.Invoke(stopwatch, methodCalled, description, user, filePath, fileName, milisseconds);

                }
                else if (saveType == "json")
                {
                    if (filePath != null)
                        throw new Exception("JSON não deve usar filePath");

                    
                    GenerateJsonEvent += _jsonGenerator.GenerateJson;

                    if (GenerateJsonEvent != null)
                    {
                        foreach (var handler in GenerateJsonEvent.GetInvocationList())
                        {
                            var logData = (JsonResult)handler.DynamicInvoke(stopwatch, methodCalled, description, user, fileName, milisseconds);
                            LastGeneratedJson = JsonConvert.SerializeObject(logData.Data, Formatting.Indented); // .Data é o conteúdo do JsonResult
                        }
                    }
                }
                else
                {
                    throw new Exception("Formato inválido. Use json ou xlsx.");
                }

            }
            finally
            {
                stopwatch.Stop();
            }
                
            return result;

        }
    }
}
