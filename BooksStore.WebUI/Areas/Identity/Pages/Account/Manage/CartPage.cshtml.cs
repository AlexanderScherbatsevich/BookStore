using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore.Domain.Entities;
using System.Text.Json;
using BookStore.Domain.Concrete;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BookStore.WebUI.Areas.Identity.Pages.Account
{
    public class CartPage : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private EFDbContext _context;

        public CartPage(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            EFDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public Cart Cart { get; set; }

        [TempData]
        public string StatusMessage { get; set; }


        //private async Task<Cart> LoadAsync(ApplicationUser user)
        //{
        //    Cart = System.Text.Json.JsonSerializer.Deserialize<Cart>(user.Cart) ?? new Cart();

        //    return Cart;
        //}

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Cart = JsonConvert.DeserializeObject<Cart>(user.Cart) ?? new Cart();
            //await LoadAsync(user);

            return Page();
        }

        //public async Task<IActionResult> OnPostAsync(Product product)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    Cart.AddItem(product, 1);
        //    string cart = JsonSerializer.Serialize(Cart);
        //    user.Cart = cart;

        //    //var newCart = JsonSerializer.Deserialize<Cart>(user.Cart);
        //    //if (Cart != newCart)
        //    //{
        //    //    string myCart = JsonSerializer.Serialize(Cart);
        //    //    user.Cart = myCart;
        //    //}

        //    await _userManager.UpdateAsync(user);
        //    await _signInManager.RefreshSignInAsync(user);
        //    StatusMessage = "Your profile has been updated";
        //    return RedirectToPage();
        //}
    }
}
