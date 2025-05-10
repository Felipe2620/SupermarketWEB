using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;


namespace SupermarketWEB.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly SupermarketContext _context;

        public CreateModel(SupermarketContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("SE HIZO POST"); // Confirmación
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Modelo inválido");
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Categories/Index");
        }

    }
}