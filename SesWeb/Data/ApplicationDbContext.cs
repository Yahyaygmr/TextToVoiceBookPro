using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SesWeb.Models.Entities;

namespace SesWeb.Data
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
