using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebUI.Components
{
    public class Navigation : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public Navigation(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Category> categories = await _categoryRepository.Categories
                .Distinct()
                .OrderBy(c => c.Name).ToListAsync();
            return View(categories);
        }
    }
}
