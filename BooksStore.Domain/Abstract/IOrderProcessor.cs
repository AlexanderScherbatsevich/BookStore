using BookStore.Domain.Entities;

namespace BookStore.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessorOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
