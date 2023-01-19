#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore.Domain.Entities;
using BookStore.Domain.Concrete;
using Newtonsoft.Json;

namespace BookStore.WebUI.Areas.Identity.Pages.Account
{
    public class ShippingDetailsPage : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly EFDbContext _dbContext;

        public ShippingDetailsPage(
            UserManager<ApplicationUser> userInManager, 
            SignInManager<ApplicationUser> signInManager,
            EFDbContext dbContext
            )
        {
            _userManager = userInManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public ShippingDetails ShippingDetails { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ShippingDetails = JsonConvert.DeserializeObject<ShippingDetails>(user.ShippingDetails) 
                ?? new ShippingDetails();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (ModelState.IsValid)
            {

                user.ShippingDetails = JsonConvert.SerializeObject(ShippingDetails);

                await _userManager.UpdateAsync(user);
                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated";

                return Page();
            }

            return Page();

        }
    }
}
