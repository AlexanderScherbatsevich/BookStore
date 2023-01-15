using BookStore.Domain.Entities;

namespace BookStore.Domain.Abstract
{
    public interface ICartRepository
    {
        Task<Cart> GetCart(Guid userId);
        void SaveProduct(Cart cart);
        Cart DeleteProduct(int cartId);
    }
}
