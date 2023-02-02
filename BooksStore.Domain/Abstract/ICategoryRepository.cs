using BookStore.Domain.Entities;

namespace BookStore.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        Task<IQueryable<Product>> GetProducts(int categoryId);
        Task SaveCategory(Category category);
        Task<Category> DeleteCategory(int categoryID);
    }
}
