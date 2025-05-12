using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using UglyToad.PdfPig;

namespace SesAPI.Services
{
    public interface IPdfReaderService
    {
        Task<string> ReadPdfTextAsync(string filePath);
    }

    public class PdfReaderService : IPdfReaderService
    {
        private readonly IWebHostEnvironment _environment;

        public PdfReaderService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> ReadPdfTextAsync(string filePath)
        {
            var fullPath = Path.Combine(_environment.WebRootPath, filePath);
            
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("PDF dosyası bulunamadı.", fullPath);
            }

            var textBuilder = new StringBuilder();

            using (var document = UglyToad.PdfPig.PdfDocument.Open(fullPath))
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