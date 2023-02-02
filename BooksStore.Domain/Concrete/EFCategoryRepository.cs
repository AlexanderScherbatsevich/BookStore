using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly EFDbContext _context;

        public EFCategoryRepository(EFDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories
        {
            get { return _context.Categories; }
        }

        public async Task<IQueryable<Product>> GetProducts(int categoryID)
        {
            Category? dbEntry = await _context.Categories.FindAsync(categoryID);

            return dbEntry.Products.AsQueryable();
        }

        public async Task SaveCategory(Category category)
        {
            if (category.Id == 0)
            {
                await _context.Categories.AddAsync(category);
            }
            else
            {
                Category? dbEntry = await _context.Categories.FindAsync(category.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                    dbEntry.Products = category.Products;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Category> DeleteCategory(int categoryID)
        {
            Category? dbEntry = await _context.Categories.FindAsync(categoryID);
            if (dbEntry != null)
            {
                _context.Categories.Remove(dbEntry);
                await _context.SaveChangesAsync();
            }
            return dbEntry;
        }
    }
}
