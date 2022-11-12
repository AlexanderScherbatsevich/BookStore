using BooksStore.Domain.Entities;

namespace BooksStore.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessorOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
