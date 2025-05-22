using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SesWeb.Data;
using SesWeb.Models.Entities;

namespace SesAPI.Controllers
{
    [Authorize]
    public class UserBooksController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public UserBooksController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var books = await _dbContext.PdfDocuments
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    FilePath = b.FilePath,
                    Progress = _dbContext.BookProgresses
                        .Where(p => p.BookId == b.Id && p.UserId == userId)
                        .Select(p => new BookProgressViewModel
                        {
                            LastPosition = p.LastPosition,
                            IsCompleted = p.IsCompleted
                        })
                        .FirstOrDefault()
                })
                .ToListAsync();

            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProgress(int bookId, double position, bool isCompleted)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var progress = await _dbContext.BookProgresses
                .FirstOrDefaultAsync(p => p.BookId == bookId && p.UserId == userId);

            if (progress == null)
            {
                progress = new BookProgress
                {
                    BookId = bookId,
                    UserId = userId,
                    LastPosition = position,
                    LastUpdated = DateTime.UtcNow,
                    IsCompleted = isCompleted
                };
                _dbContext.BookProgresses.Add(progress);
            }
            else
            {
                progress.LastPosition = position;
                progress.LastUpdated = DateTime.UtcNow;
                progress.IsCompleted = isCompleted;
            }

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }

    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string FilePath { get; set; }
        public BookProgressViewModel Progress { get; set; }
    }

    public class BookProgressViewModel
    {
        public double LastPosition { get; set; }
        public bool IsCompleted { get; set; }
    }
} 