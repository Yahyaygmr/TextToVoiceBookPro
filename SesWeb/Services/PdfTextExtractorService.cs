using System.IO;
using UglyToad.PdfPig;

namespace SesWeb.Services
{
    public class PdfTextExtractorService
    {
        public string GetTextFromPdf(string filePath)
        {
            var textBuilder = new System.Text.StringBuilder();

            using (var document = PdfDocument.Open(filePath))
            {
                foreach (var page in document.GetPages())
                {
                    textBuilder.AppendLine(page.Text);
                }
            }

            return textBuilder.ToString();
        }
    }
} 