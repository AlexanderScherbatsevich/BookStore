using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a book title")]
        [MaxLength(200)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter an author name")]
        [MaxLength(200)]
        public string Author { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public byte[]? ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        [MaxLength(255)]
        public string? ImageMimeType { get; set; }
    }
}
