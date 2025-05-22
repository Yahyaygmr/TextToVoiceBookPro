using System.ComponentModel.DataAnnotations;

namespace SesWeb.Models.Entities
{
    public class PdfDocument
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string FilePath { get; set; }

        public DateTime UploadDate { get; set; }

        public string UploadedBy { get; set; }
    }
}
