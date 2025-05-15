using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Models;
using System.Security.Claims;

namespace SupermarketWEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Users Users { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Fix: Use Users property instead of ClaimsPrincipal User  
            if (Users.Email == "Correo@gmail.com" && Users.Password == "12345")
                return RedirectToPage("/Index");

            var Claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.Email, Users.Email)
            };
            var Identity = new ClaimsIdentity(Claim, "MyCookieAuth");
            ClaimsPrincipal ClaimsPrincipal = new ClaimsPrincipal(Identity);

            await HttpContext.SignInAsync("MyCookieAuth", ClaimsPrincipal);
            return RedirectToPage("/Index");
            return Page();
            return Page();
        }
    }
}
