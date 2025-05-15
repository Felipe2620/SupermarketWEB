using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity; // <-- Agrega este using
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
        private readonly ILogger<LoginModel> _logger;
        private readonly SupermarketContext _context;

        public LoginModel(SupermarketContext context, ILogger<LoginModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Users Users { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync ejecutado");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState inválido");
                return Page();
            }

            var userInDb = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Users.Email);

            if (userInDb == null)
            {
                _logger.LogWarning("Usuario no encontrado: {Email}", Users.Email);
            }
            else
            {
                var hasher = new PasswordHasher<Users>();
                var result = hasher.VerifyHashedPassword(userInDb, userInDb.Password, Users.Password);

                _logger.LogInformation("Resultado verificación de contraseña: {Result}", result);

                if (result == PasswordVerificationResult.Success)
                {
                    _logger.LogInformation("Login exitoso para: {Email}", userInDb.Email);

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
                else
                {
                    _logger.LogWarning("Contraseña incorrecta para: {Email}", userInDb.Email);
                }
            }

            ModelState.AddModelError(string.Empty, "Correo o contraseña inválidos");
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        _logger.LogWarning("Error en '{Key}': {ErrorMessage}", key, error.ErrorMessage);
                    }
                }
                _logger.LogWarning("ModelState inválido");
                return Page();
            }
            return Page();
        }
    }
}