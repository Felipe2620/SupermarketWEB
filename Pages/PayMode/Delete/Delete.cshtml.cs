using SupermarketWEB.Data;
using SupermarketWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SupermarketWEB.Pages.PayMode
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SupermarketWEB.Models.PayMode PayMode { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PayMode == null)
            {
                return NotFound();
            }

            var payMode = await _context.PayMode.FirstOrDefaultAsync(m => m.Id == id);

            if (payMode == null)
            {
                return NotFound();
            }
            else
            {
                PayMode = payMode; // Correctly assign the retrieved PayMode to the property
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.PayMode == null || PayMode == null || PayMode.Id == 0)
            {
                return NotFound();
            }

            var payMode = await _context.PayMode.FindAsync(PayMode.Id);

            if (payMode != null)
            {
                _context.PayMode.Remove(payMode);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/PayMode/Visualizar/Index");
        }
    }
}
