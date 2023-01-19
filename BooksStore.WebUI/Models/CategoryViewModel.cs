using BookStore.Domain.Entities;

namespace BookStore.WebUI.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
    }
}
