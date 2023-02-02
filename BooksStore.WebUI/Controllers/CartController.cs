using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace BookStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        //private IOrderProcessor _orderProcessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IProductRepository repo, /*IOrderProcessor proc,*/ UserManager<ApplicationUser> userManager)
        {
            _repository = repo;
            //_orderProcessor = proc;
            _userManager = userManager;
        }

        //public CartController(IProductRepository repo)
        //{
        //    _repository = repo;
        //}

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int Id, string returnUrl = "~/")
        {
            var user = await _userManager.GetUserAsync(User);

            Cart cart = JsonConvert.DeserializeObject<Cart>(user.Cart) ?? new Cart();  
            Product? product = _repository.Products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {
                cart.AddItem(product, 1);

                //user.Cart = string.Empty;
                //await _userManager.UpdateAsync(user);
                string newCartJson = JsonConvert.SerializeObject(cart);
                user.Cart = newCartJson;
                await _userManager.UpdateAsync(user);
            }
            return Redirect("/Product/List");
        }

        public async Task<IActionResult> RemoveFromCart(int ProductId, string returnUrl = "~/")
        {
            var user = await _userManager.GetUserAsync(User);

            Cart cart = JsonConvert.DeserializeObject<Cart>(user.Cart) ?? new Cart();

            Product? product = _repository.Products.FirstOrDefault(p => p.Id == ProductId);
            if (product != null)
                cart.RemoveLine(product);

            string newCartJson = JsonConvert.SerializeObject(cart);
            user.Cart = newCartJson;
            await _userManager.UpdateAsync(user);

            return Redirect("/Identity/Account/Manage/CartPage");
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (ModelState.IsValid)
            {
                //_orderProcessor.ProcessorOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}
