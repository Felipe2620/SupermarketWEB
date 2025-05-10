using SupermarketWEB.Data;
using SupermarketWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SupermarketWEB.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SupermarketWEB.Models.Product Products { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Products = product; // Correctly assign the retrieved product to the property
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Products == null || Products == null || Products.Id == 0)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(Products.Id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Products/Visualizar/Index");
        }
    }
}
