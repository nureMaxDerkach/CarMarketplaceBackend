namespace Application.Services.ExcelService;

public interface IExcelService
{
    byte[] GenerateExcelByteArray<T>(List<T> data, string sheetName);
}