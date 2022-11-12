using BooksStore.Domain.Abstract;
using BooksStore.Domain.Entities;

namespace BooksStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext _context;

        public EFProductRepository(EFDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products
        {
            get { return _context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products.Find(product.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Author = product.Author;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    //dbEntry.ImageData = product.ImageData;
                    //dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            _context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = _context.Products.Find(productID);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
