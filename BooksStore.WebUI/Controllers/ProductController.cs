using BookStore.Domain.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.WebUI.Models;
using BookStore.Domain.Entities;

namespace BookStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> List(int? category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = await _productRepository.Products
                .Include(c => c.Category)
                .Where(c => category == null || c.Category.Id == category)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = await _productRepository.Products.CountAsync()
                },
                CurrentCategoryId = category
            };
            return View(model);
        }
    }
}
