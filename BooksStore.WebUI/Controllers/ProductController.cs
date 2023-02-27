using BookStore.Domain.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.WebUI.Models;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> List(string searchString, int? category, int page = 1)
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
                    TotalItems = await _productRepository.Products
                        .Include(c => c.Category)
                        .Where(c => category == null || c.Category.Id == category)
                        .CountAsync()
                },
                CurrentCategoryId = category
            };


            if (!String.IsNullOrEmpty(searchString))
            {
                model = new ProductsListViewModel
                {
                    Products = await _productRepository.Products
                        .Include(c => c.Category)
                        .Where(c => category == null || c.Category.Id == category)
                        .Where(p => p.Name.Contains(searchString) || p.Author.Contains(searchString))
                        .OrderBy(p => p.Id)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .ToListAsync(),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = await _productRepository.Products
                        .Include(c => c.Category)
                        .Where(c => category == null || c.Category.Id == category)
                        .Where(p => p.Name.Contains(searchString) || p.Author.Contains(searchString))
                        .CountAsync()
                    },
                    CurrentCategoryId = category
                };
            }

            return View(model);
        }

        public IActionResult Index()
        {
            return View(_productRepository.Products.Include(c => c.Category));
        }

        public async Task<IActionResult> Edit(int productId)
        {
            IEnumerable<Category> categories = _categoryRepository.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            Product? product = await _productRepository.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.Length];
                    await image.OpenReadStream().ReadAsync(product.ImageData, 0, (int)image.Length);
                }
                await _productRepository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                //something wrong with data values
                return View(product);
            }
        }

        public IActionResult Create()
        {
            IEnumerable<Category> categories = _categoryRepository.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View("Edit", new Product());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int productID)
        {
            Product deleteProduct = await _productRepository.DeleteProduct(productID);
            if (deleteProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deleteProduct.Name);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetImage(int Id)
        {
            Product? prod = await _productRepository.Products
                .FirstOrDefaultAsync(p => p.Id == Id);
            if (prod != null)
                return File(prod.ImageData, prod.ImageMimeType);
            else
                return null;
        }
    }
}
