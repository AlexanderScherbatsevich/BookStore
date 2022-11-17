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

        public IQueryable<Product> GetProducts(int categoryID)
        {
            Category dbEntry = _context.Categories.Find(categoryID);

            return dbEntry.Products.AsQueryable();
        }

        public void SaveCategory(Category category)
        {
            if (category.Id == 0)
            {
                _context.Categories.Add(category);
            }
            else
            {
                Category dbEntry = _context.Categories.Find(category.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                    dbEntry.Products = category.Products;
                }
            }
            _context.SaveChanges();
        }

        public Category DeleteCategory(int categoryID)
        {
            Category dbEntry = _context.Categories.Find(categoryID);
            if (dbEntry != null)
            {
                _context.Categories.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
