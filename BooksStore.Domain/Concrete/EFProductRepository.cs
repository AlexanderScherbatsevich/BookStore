using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext _context;

        public EFProductRepository(EFDbContext context)
        {
            _context = context;
        }

        public  IQueryable<Product> Products
        {
            get { return  _context.Products; }
        }

        public async Task SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                await _context.Products.AddAsync(product);
            }
            else
            {
                Product? dbEntry = await _context.Products.FindAsync(product.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Author = product.Author;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.CategoryId = product.CategoryId;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Product> DeleteProduct(int productID)
        {
            Product? dbEntry = await _context.Products.FindAsync(productID);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                await _context.SaveChangesAsync();
            }
            return dbEntry;
        }
    }
}
