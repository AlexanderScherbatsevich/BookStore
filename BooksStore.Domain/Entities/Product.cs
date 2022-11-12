using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksStore.Domain.Entities
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
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public Category Category { get; set; }

        //public byte[] ImageData { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public string ImageMimeType { get; set; }
    }
}
