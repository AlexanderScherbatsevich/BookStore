using BookStore.Domain.Entities;

namespace BookStore.Domain.Abstract
{
    public interface IShippingDetailsRepository
    {
        ShippingDetails GetShippingDetails(Guid userId);
        void SaveProduct(ShippingDetails shippingDetails);
        ShippingDetails DeleteProduct(int shippingDetailsId);
    }
}
