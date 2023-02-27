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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Cart = JsonConvert.DeserializeObject<Cart>(user.Cart) ?? new Cart();

            return Page();
        }
    }
}
