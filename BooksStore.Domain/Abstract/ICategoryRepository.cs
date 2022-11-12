using BooksStore.Domain.Entities;

namespace BooksStore.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
        void SaveCategory(Category category);
        Category DeleteCategory(int categoryID);
    }
}
