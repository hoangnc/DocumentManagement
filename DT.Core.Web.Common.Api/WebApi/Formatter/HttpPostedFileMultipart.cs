using System.IO;
using System.Web;

namespace DT.Core.Web.Common.Api.WebApi.Formatter
{
    /// <summary>
    /// Represents a file that has uploaded by a client via multipart/form-data. 
    /// </summary>
    public class HttpPostedFileMultipart : HttpPostedFileBase
    {
        private readonly MemoryStream _fileContents;

        public override int ContentLength => (int)_fileContents.Length;
        public override string ContentType { get; }
        public override string FileName { get; }
        public override Stream InputStream => _fileContents;

        public override void SaveAs(string filename)
        {
            using (FileStream file = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                _fileContents.WriteTo(file);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPostedFileMultipart"/> class. 
        /// </summary>
        /// <param name="fileName">The fully qualified name of the file on the client</param>
        /// <param name="contentType">The MIME content type of an uploaded file</param>
        /// <param name="fileContents">The contents of the uploaded file.</param>
        public HttpPostedFileMultipart(string fileName, string contentType, byte[] fileContents)
        {
            FileName = fileName;
            ContentType = contentType;
            _fileContents = new MemoryStream(fileContents);
        }
    }
}
