using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SesAPI.Models;

namespace SesAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PdfDocument> PdfDocuments { get; set; }
        public DbSet<BookProgress> BookProgresses { get; set; }
        public DbSet<Book> Books { get; set; }
    }
} 