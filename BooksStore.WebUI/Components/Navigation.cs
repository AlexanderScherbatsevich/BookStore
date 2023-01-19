using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BookStore.WebUI.Models;

namespace BookStore.WebUI.Components
{
    public class Navigation : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public Navigation(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
        {
            IEnumerable<Category> categories = await _categoryRepository.Categories
                .Distinct()
                .OrderBy(c => c.Name).ToListAsync();

            CategoryViewModel categoryVM = new CategoryViewModel
            {
                Categories = categories,
                SelectedCategoryId = categoryId
            };
            return View("Navigation", categoryVM);
        }
    }
}
