using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayMode
{
    public class EditModel : PageModel
    {
        private readonly SupermarketContext _context;
        public EditModel(SupermarketContext context)
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
            
            var PayMode = await _context.PayMode.FirstOrDefaultAsync(m => m.Id == id);
            if (PayMode == null)
            {
                return NotFound();
            }
            PayMode = PayMode;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(PayMode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayModeExists(PayMode.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("/PayMode/Visualizar/Index");
        }
        private bool PayModeExists(int id)
        {
            return (_context.PayMode?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}