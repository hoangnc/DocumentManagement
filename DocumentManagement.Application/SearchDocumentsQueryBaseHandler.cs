using DT.Core.Helper;
using DT.Core.Text;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Xceed.Words.NET;

namespace DocumentManagement.Application
{
    public abstract class SearchDocumentsQueryBaseHandler
    {
        protected List<string> FindFilesByToken(string token)
        {
            string folder = HttpContext.Current.Server.MapPath("~/" + $"Uploads");
            List<string> fileNames = new List<string>();
            if (Directory.Exists(folder))
            {
                string[] subDirectories = Directory.GetDirectories(folder);
                if (subDirectories != null && subDirectories.Any())
                {
                    foreach (string directory in subDirectories)
                    {
                        string[] filePaths = Directory.GetFiles(directory);
                        if (filePaths != null && filePaths.Any())
                        {
                            foreach (string filePath in filePaths)
                            {
                                if (!filePath.IsNullOrEmpty())
                                {
                                    string fileExtension = Path.GetExtension(filePath).ToLowerInvariant();
                                    switch (fileExtension)
                                    {
                                        case ".pdf":
                                            FindPdfFileByToken(fileNames, filePath, token);
                                            break;
                                        case ".xls":
                                            FindExcelFileByToken(fileNames, filePath, token);
                                            break;
                                        case ".xlsx":
                                            FindExcelFileByToken(fileNames, filePath, token);
                                            break;
                                        case ".doc":
                                            FindWordFileByToken(fileNames, filePath, token);
                                            break;
                                        case ".docx":
                                            FindWordFileByToken(fileNames, filePath, token);
                                            break;
                                        default:
                                            FindTextFileByToken(fileNames, filePath, token);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                throw new DirectoryNotFoundException(folder);
            return fileNames;
        }

        protected void FindPdfFileByToken(List<string> files, string filePath, string token)
        {
            string pdfContent = PdfHelper.ExtractTextFromPdf(filePath)?.ToLowerInvariant()?.NonUnicode();
            if (pdfContent.Contains(token))
            {
                files.Add(Path.GetFileName(filePath));
            }
        }

        protected void FindWordFileByToken(List<string> files, string filePath, string token)
        {
            DocX docX = DocX.Load(filePath);
            string text = docX.Text?.ToLowerInvariant()?.NonUnicode();
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Contains(token))
                    files.Add(Path.GetFileName(filePath));
            }
        }

        protected void FindTextFileByToken(List<string> files, string filePath, string token)
        {
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath)?.ToLowerInvariant()?.NonUnicode();
                if (!text.IsNullOrEmpty())
                {
                    if (text.Contains(token))
                        files.Add(Path.GetFileName(filePath));
                }
            }
        }

        protected void FindExcelFileByToken(List<string> files, string filePath, string token)
        {
            FileInfo existingFile = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                {
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count
                    for (int row = 1; row <= rowCount; row++)
                    {
                        for (int col = 1; col <= colCount; col++)
                        {
                            string cellValue = worksheet.Cells[row, col].Value?.ToString();
                            if (!cellValue.IsNullOrEmpty())
                            {
                                if (cellValue.ToLowerInvariant().NonUnicode()
                                    .Contains(token))
                                {
                                    files.Add(Path.GetFileName(filePath));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
