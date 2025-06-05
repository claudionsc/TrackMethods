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

            try
            {
                Console.WriteLine("Aguarde a geração do arquivo");
                var result = func();



                if (func == null)
                {
                stopwatch.Stop();
                    throw new Exception("É preciso selecionar um método com retorno");
                }

                if (saveType == "xlsx" || saveType == "excel")
                {
                    if (filePath == null)
                    {
                        stopwatch.Stop();
                        throw new Exception("Insira a pasta onde o arquivo será salvo");
                    }
                    SaveExcel(stopwatch, methodCalled, description, user, filePath, fileName, milisseconds);
                    stopwatch.Stop();
                    return result;
                }
                if(saveType == "json")
                {
                    if(filePath == null)
                    {
                        var jsonObject = GenerateJson(stopwatch, methodCalled, description, user, fileName, milisseconds);
                        LastGeneratedJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

                    }
                    else
                    {
                    stopwatch.Stop();
                    throw new Exception("Não é possível salvar um arquivo do tipo JSON. Exclua o parâmetro filePath");

                    }

                }
                else
                {
                    stopwatch.Stop();
                    throw new Exception("Só é possivel salvar como xlsx ou JSON");
                }

                return result;

            }
            catch (Exception e)
            {
                throw new Exception("Erro: " + e.Message);
            }
        }

        public void SaveExcel(Stopwatch sw, string methodCalled, string description, string user, string saveFilePath, string fileName, Int32 milisseconds)
        {
            var fileExists = File.Exists(saveFilePath);
            using var workbook = fileExists ? new XLWorkbook(saveFilePath) : new XLWorkbook();

            var worksheet = workbook.Worksheets.Contains("Logs salvos")
                ? workbook.Worksheet("Logs salvos")
                : workbook.Worksheets.Add("Logs salvos");

            if (worksheet.Cell(1, 1).IsEmpty())
            {
                worksheet.Cell(1, 1).Value = "Timestamp";
                worksheet.Cell(1, 2).Value = "Tempo de Resposta";
                worksheet.Cell(1, 3).Value = "Método observado";
                worksheet.Cell(1, 4).Value = "Descrição";
                worksheet.Cell(1, 5).Value = "Salvo por";
                worksheet.Cell(1, 6).Value = "Nome do arquivo";
                worksheet.Cell(1, 7).Value = "Tempo esperado(ms)";

                worksheet.Range("A1:G1").Style.Font.Bold = true;
                worksheet.Range("A1:G1").Style.Fill.BackgroundColor = XLColor.LightGray;
            }

            int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;
            int nextRow = lastRow + 1;

            worksheet.Cell(nextRow, 1).Value = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
            worksheet.Cell(nextRow, 2).Value = FormatElapsed(sw.Elapsed);
            worksheet.Cell(nextRow, 3).Value = methodCalled;
            worksheet.Cell(nextRow, 4).Value = description;
            worksheet.Cell(nextRow, 5).Value = user;
            worksheet.Cell(nextRow, 6).Value = fileName;
            worksheet.Cell(nextRow, 7).Value = milisseconds;

            // Estilo condicional: tempo de resposta > milisseconds
            var range = worksheet.Range($"B2:B{nextRow}");
            foreach (var cell in range.Cells())
            {
                if (TimeSpan.TryParseExact(cell.GetValue<string>(), @"hh\:mm\:ss\.ff", null, out var time) && time.TotalMilliseconds > milisseconds)
                {
                    cell.Style.Fill.BackgroundColor = XLColor.DarkRed;
                    cell.Style.Font.FontColor = XLColor.White;
                }
            }

            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(saveFilePath);

            Console.WriteLine("Arquivo salvo no caminho " + saveFilePath);
        }

        public JsonResult GenerateJson(Stopwatch sw, string methodCalled, string description, string user, string fileName, Int32 milisseconds)
        {
            string elapsedTime = FormatElapsed(sw.Elapsed);
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

        private string FormatElapsed(TimeSpan ts)
        {
            return string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }
    }
}
