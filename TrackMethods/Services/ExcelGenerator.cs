using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMethods.Interfaces;

namespace TrackMethods.Services
{
    internal class ExcelGenerator : IExcelGenerator
    {
        public ExcelGenerator()
        {
            
        }

        public void GenerateExcel(Stopwatch sw, string methodCalled, string description, string user, string saveFilePath, string fileName, Int32 milisseconds)
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

            var elapsedFormatted = FormatElapsed.ElapsedFormatter(sw.Elapsed);
            var expectedTimeSpan = TimeSpan.FromMilliseconds(milisseconds);
            var expectedFormatted = FormatElapsed.ElapsedFormatter(expectedTimeSpan);


            worksheet.Cell(nextRow, 1).Value = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
            worksheet.Cell(nextRow, 2).Value = elapsedFormatted;
            worksheet.Cell(nextRow, 3).Value = methodCalled;
            worksheet.Cell(nextRow, 4).Value = description;
            worksheet.Cell(nextRow, 5).Value = user;
            worksheet.Cell(nextRow, 6).Value = fileName;
            worksheet.Cell(nextRow, 7).Value = expectedFormatted;

            // Estilo condicional: tempo de resposta > milisseconds
            var range = worksheet.Range($"B2:B{nextRow}");
            foreach (var cell in range.Cells())
            {
                if (TimeSpan.TryParseExact(cell.GetValue<string>(), @"hh\:mm\:ss\.fff", null, out var time) && time > expectedTimeSpan)
                {
                    cell.Style.Fill.BackgroundColor = XLColor.DarkRed;
                    cell.Style.Font.FontColor = XLColor.White;
                }
            }

            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(saveFilePath);

            Console.WriteLine("Arquivo salvo no caminho " + saveFilePath);
        }
    }
}
