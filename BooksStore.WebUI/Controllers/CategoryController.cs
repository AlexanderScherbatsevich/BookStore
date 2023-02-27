using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Categories);
        }

        public IActionResult Edit(int categoryId)
        {
            Category? category = _repository.Categories.FirstOrDefault(c => c.Id == categoryId);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveCategory(category);
                TempData["message"] = string.Format("{0} has been saved", category.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public IActionResult Create()
        {
            return View("Edit", new Category());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int categoryId)
        {
            Category deleteCategoy = await _repository.DeleteCategory(categoryId);
            if (deleteCategoy != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deleteCategoy.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
