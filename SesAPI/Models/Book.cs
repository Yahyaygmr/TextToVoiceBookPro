using System;
using System.ComponentModel.DataAnnotations;

namespace SesAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required]
        public string PdfFilePath { get; set; }

        [Required]
        public string AudioFilePath { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
} 