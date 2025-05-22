using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SesWeb.Data;
using SesWeb.Models.Entities;
using SesWeb.Services;

namespace SesAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBooksController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PdfTextExtractorService _pdfTextExtractorService;
        private readonly ITextToSpeechService _textToSpeechService;
        private readonly IWebHostEnvironment _environment;

        public AdminBooksController(ApplicationDbContext dbContext, PdfTextExtractorService pdfTextExtractorService, ITextToSpeechService textToSpeechService, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _pdfTextExtractorService = pdfTextExtractorService;
            _textToSpeechService = textToSpeechService;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _dbContext.PdfDocuments.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(Book model, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "PDF dosyası seçilmedi.");
                return View(model);
            }

            var pdfDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf");
            if (!Directory.Exists(pdfDir))
                Directory.CreateDirectory(pdfDir);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(pdfDir, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            model.PdfFilePath = Path.Combine("pdf", fileName);
            model.CreatedAt = DateTime.UtcNow;

            // 1. PDF'den metin çıkar
            var fullPdfPath = Path.Combine(_environment.WebRootPath, "pdf", fileName);
            var pdfText = _pdfTextExtractorService.GetTextFromPdf(fullPdfPath);

            // 2. Metni sese dönüştür ve ses dosyasını kaydet
            var audioFileName = $"audio_{Guid.NewGuid()}.wav";
            var audioFilePath = Path.Combine(_environment.WebRootPath, "audio", audioFileName);
            if (!Directory.Exists(Path.GetDirectoryName(audioFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(audioFilePath));
            await _textToSpeechService.GenerateMp3Async(pdfText, audioFileName);

            // 3. Ses dosyasının yolunu Book tablosuna kaydet
            model.AudioFilePath = Path.Combine("audio", audioFileName);

            _dbContext.Books.Add(model);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _dbContext.PdfDocuments.FindAsync(id);
            if (book != null)
            {
                _dbContext.PdfDocuments.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
} 