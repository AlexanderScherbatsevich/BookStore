using BookStore.Domain.Entities;

namespace BookStore.WebUI.Models
{
    public class ManagedProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable <Category> Categories { get; set; }
        public Category SelectedCategory { get; set; }
    }
}
