using DT.Core.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;

namespace DT.Core.Helper
{
    public static class PdfHelper
    {
        public static string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string text = PdfTextExtractor.GetTextFromPage(reader, i);
                    if (!text.IsNullOrEmpty())
                    {
                        stringBuilder.AppendLine(text);
                    }
                }

                return stringBuilder.ToString();
            }
        }
    }
}
