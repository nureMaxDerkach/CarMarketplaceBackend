using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Application.Services.ExcelService;

public class ExcelService : IExcelService
{
    public byte[] GenerateExcelByteArray<T>(List<T> data, string sheetName)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            var properties = typeof(T).GetProperties();
            
            for (int i = 0; i < properties.Length; i++)
            {
                var cell = worksheet.Cells[1, i + 1];
                cell.Value = properties[i].Name;
                cell.Style.Font.Bold = true; 
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid; 
                cell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; 
            }

            for (var row = 0; row < data.Count; row++)
            {
                for (var col = 0; col < properties.Length; col++)
                {
                    worksheet.Cells[row + 2, col + 1].Value = properties[col].GetValue(data[row]);
                }
            }

            worksheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}