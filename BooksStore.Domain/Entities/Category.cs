using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a category name")]
        [MaxLength(20)]
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
