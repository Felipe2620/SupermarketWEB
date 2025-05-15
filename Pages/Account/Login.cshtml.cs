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

        [BindProperty]
        public bool RememberMe { get; set; }  // <-- propiedad para checkbox

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Validar credenciales
            if (Users.Email == "Correo@gmail.com" && Users.Password == "12345")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Email, Users.Email)
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                // Aquí pasamos el parámetro RememberMe para controlar duración de la cookie
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = RememberMe // persistir cookie si el usuario marca "Recuérdame"
                };

                await HttpContext.SignInAsync("MyCookieAuth", principal, authProperties);

                return RedirectToPage("/Index");
            }

            // Si las credenciales no son válidas
            ModelState.AddModelError(string.Empty, "Correo o contraseña inválidos");
            return Page();
        }
    }
}
