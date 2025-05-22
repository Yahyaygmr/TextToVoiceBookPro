using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SesAPI.Models
{
    public class BookProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public double LastPosition { get; set; } // Saniye cinsinden son dinlenen pozisyon

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        // Navigation properties
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
} 