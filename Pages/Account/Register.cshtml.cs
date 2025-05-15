using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity; // <-- Agrega este using
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SupermarketContext _context;

        public RegisterModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Users User { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var exists = _context.Users.Any(u => u.Email == User.Email);
            if (exists)
            {
                ModelState.AddModelError(string.Empty, "Ya existe una cuenta con este correo.");
                return Page();
            }

            // Hashear la contraseña antes de guardar
            var hasher = new PasswordHasher<Users>();
            User.Password = hasher.HashPassword(User, User.Password);

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("Login");
        }
    }
}