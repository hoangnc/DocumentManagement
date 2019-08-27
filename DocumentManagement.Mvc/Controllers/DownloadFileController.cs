using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Controllers
{
    [Authorize]
    public class DownloadFileController : Controller
    {
        // GET: DownloadFile
        public FileResult ViewFile(string sourceDoc)
        {
            string uploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
            string filePath = $"{uploadFolderPath}\\{sourceDoc.Replace("/","\\")}";
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string mime = MimeMapping.GetMimeMapping(sourceDoc);
            if(mime == "application/pdf")
            {
                var cd = new ContentDisposition { FileName = Path.GetFileName(sourceDoc), Inline = true };
                Response.AppendHeader("Content-Disposition", cd.ToString());
                return File(fileBytes, MimeMapping.GetMimeMapping(sourceDoc));
            }
            return File(fileBytes, MimeMapping.GetMimeMapping(sourceDoc), Path.GetFileName(sourceDoc));
        }
    }
}