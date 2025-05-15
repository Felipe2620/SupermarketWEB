using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Security.Claims;

namespace SupermarketWEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SupermarketContext _context;

        public LoginModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Users Users { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var userInDb = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Users.Email);

            if (userInDb != null && userInDb.Password == Users.Password) // Mejora con hash
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInDb.Name ?? "Usuario"),
                    new Claim(ClaimTypes.Email, userInDb.Email)
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = RememberMe
                };

                await HttpContext.SignInAsync("MyCookieAuth", principal, authProperties);

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Correo o contraseña inválidos");
            return Page();
        }
    }
}
