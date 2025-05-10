using SupermarketWEB.Data;
using SupermarketWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SupermarketWEB.Pages.Providers
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SupermarketWEB.Models.Product Providers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return NotFound();
            }

            var product = await _context.Providers.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Providers = product; // Correctly assign the retrieved product to the property
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Providers == null || Providers == null || Providers.Id == 0)
            {
                return NotFound();
            }

            var product = await _context.Providers.FindAsync(Providers.Id);

            if (product != null)
            {
                _context.Providers.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Providers/Visualizar/Index");
        }
    }
}
