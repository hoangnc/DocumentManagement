using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Data;
using DT.Core.Data.Models;
using DT.Core.Data.Paged;
using DT.Core.Helper;
using DT.Core.Text;
using MediatR;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Xceed.Words.NET;

namespace DocumentManagement.Application.Documents.Queries
{
    public class SearchDocumentsByTokenPagedQueryHandler : IRequestHandler<SearchDocumentsByTokenPagedQuery, DataSourceResult>
    {
        private readonly DocumentDbContext _context;
        public SearchDocumentsByTokenPagedQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(SearchDocumentsByTokenPagedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Document> query = _context.Documents.AsQueryable();
            query = query.Where(c => !c.Deleted);

            request.Token = request.Token?.ToLowerInvariant()?.NonUnicode();

            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                if (request.AdvancedSearch)
                {
                    string fileNames = string.Join(";", FindFilesByToken(request.Token));

                    query = query.Where(c => _context.NonUnicode(c.Name).Contains(request.Token)
                            || _context.NonUnicode(c.ContentChange).Contains(request.Token)
                            || _context.NonUnicode(c.DepartmentName).Contains(request.Token)
                            || _context.NonUnicode(c.DocumentNumber).Contains(request.Token)
                            || _context.NonUnicode(c.CompanyName).Contains(request.Token)
                            || _context.NonUnicode(c.FileName).Contains(request.Token)
                            || _context.NonUnicode(c.FolderName).Contains(request.Token)
                            || _context.NonUnicode(c.LinkFile).Contains(request.Token)
                            || _context.NonUnicode(c.Module).Contains(request.Token)
                            || _context.NonUnicode(c.RelateToDocuments).Contains(request.Token)
                            || _context.NonUnicode(c.ReplaceOf).Contains(request.Token)
                            || _context.NonUnicode(c.ReviewNumber).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfApplication).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfDeloyment).Contains(request.Token)
                            || _context.NonUnicode(c.Description).Contains(request.Token)
                            || _context.CompareTwoFiles(_context.NonUnicode(c.FileName), fileNames, ";"));
                }
                else
                {
                    query = query.Where(c => _context.NonUnicode(c.Name).Contains(request.Token)
                            || _context.NonUnicode(c.ContentChange).Contains(request.Token)
                            || _context.NonUnicode(c.DepartmentName).Contains(request.Token)
                            || _context.NonUnicode(c.DocumentNumber).Contains(request.Token)
                            || _context.NonUnicode(c.CompanyName).Contains(request.Token)
                            || _context.NonUnicode(c.FileName).Contains(request.Token)
                            || _context.NonUnicode(c.FolderName).Contains(request.Token)
                            || _context.NonUnicode(c.LinkFile).Contains(request.Token)
                            || _context.NonUnicode(c.Module).Contains(request.Token)
                            || _context.NonUnicode(c.RelateToDocuments).Contains(request.Token)
                            || _context.NonUnicode(c.ReplaceOf).Contains(request.Token)
                            || _context.NonUnicode(c.ReviewNumber).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfApplication).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfDeloyment).Contains(request.Token)
                            || _context.NonUnicode(c.Description).Contains(request.Token));
                }
            }

            if (!request.DataSourceRequest.SortDataField.IsNullOrEmpty())
            {
                if (QueryHelper.PropertyExists<Document>(request.DataSourceRequest.SortDataField))
                {
                    switch (request.DataSourceRequest.SortOrder)
                    {
                        case "asc":
                            query = QueryHelper.OrderByProperty(query, request.DataSourceRequest.SortDataField);
                            break;
                        case "desc":
                            query = QueryHelper.OrderByPropertyDescending(query, request.DataSourceRequest.SortDataField);
                            break;
                        default:
                            query = query.OrderByDescending(u => u.CreatedOn);
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderByDescending(u => u.CreatedOn);
            }

            PagedList<Document> queryResult = new PagedList<Document>();
            await queryResult.CreateAsync(query, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            List<SearchDocumentsByTokenPagedDto> data = queryResult.Select(u => u.ToSearchDocumentsByTokenPagedDto()).ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }

        private List<string> FindFilesByToken(string token)
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

        private void FindPdfFileByToken(List<string> files, string filePath, string token)
        {
            string pdfContent = PdfHelper.ExtractTextFromPdf(filePath)?.ToLowerInvariant()?.NonUnicode();
            if (pdfContent.Contains(token))
            {
                files.Add(Path.GetFileName(filePath));
            }
        }

        private void FindWordFileByToken(List<string> files, string filePath, string token)
        {
            DocX docX = DocX.Load(filePath);
            string text =  docX.Text?.ToLowerInvariant()?.NonUnicode();
            if(!string.IsNullOrEmpty(text))
            {
                if (text.Contains(token))
                    files.Add(Path.GetFileName(filePath));
            }        
        }

        private void FindTextFileByToken(List<string> files, string filePath, string token)
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

        private void FindExcelFileByToken(List<string> files, string filePath, string token)
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
