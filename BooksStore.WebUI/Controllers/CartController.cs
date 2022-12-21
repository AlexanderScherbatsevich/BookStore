//using BookStore.Domain.Abstract;
//using BookStore.Domain.Entities;
//using BookStore.WebUI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace BookStore.WebUI.Controllers
//{
//    public class CartController : Controller
//    {
//        private IProductRepository _repository;
//        //private IOrderProcessor _orderProcessor;

//        //public CartController(IProductRepository repo, IOrderProcessor proc)
//        //{
//        //    _repository = repo;
//        //    _orderProcessor = proc;
//        //}

//        public CartController(IProductRepository repo)
//        {
//            _repository = repo;
//        }

//        public IActionResult Index(Cart cart, string returnUrl)
//        {
//            return View(new CartIndexViewModel
//            {
//                Cart = cart,
//                ReturnUrl = returnUrl
//            });
//        }

//        //public IActionResult AddToCart(int Id, string returnUrl)
//        //{
//        //    Cart cart = new Cart();
//        //    Product? product = _repository.Products.FirstOrDefault(p => p.Id == Id);
//        //    if (product != null)
//        //    {
//        //        cart.AddItem(product, 1);
//        //    }

//        //    return RedirectToAction("Index", new { cart, returnUrl });
//        //}

//        //public IActionResult RemoveFromCart(Cart cart, int productID, string returnUrl)
//        //{
//        //    Product? product = _repository.Products.FirstOrDefault(p => p.Id == productID);
//        //    if (product != null)
//        //        cart.RemoveLine(product);
//        //    return RedirectToAction("Index", new { returnUrl });
//        //}

//        public PartialViewResult Summary(Cart cart)
//        {
//            return PartialView(cart);
//        }

//        //public ViewResult Checkout()
//        //{
//        //    return View(new ShippingDetails());
//        //}

//        //[HttpPost]
//        //public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
//        //{
//        //    if (cart.Lines.Count() == 0)
//        //        ModelState.AddModelError("", "Sorry, your cart is empty!");

//        //    if (ModelState.IsValid)
//        //    {
//        //        _orderProcessor.ProcessorOrder(cart, shippingDetails);
//        //        cart.Clear();
//        //        return View("Completed");
//        //    }
//        //    else
//        //    {
//        //        return View(shippingDetails);
//        //    }
//        //}
//    }
//}
