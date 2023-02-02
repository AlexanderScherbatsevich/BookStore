using BookStore.Domain.Entities;

namespace BookStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        Task SaveProduct(Product product);
        Task<Product> DeleteProduct(int productID);
    }
}
