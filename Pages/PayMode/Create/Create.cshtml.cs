using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;


namespace SupermarketWEB.Pages.PayMode
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
        public SupermarketWEB.Models.PayMode PayMode { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.PayMode == null || PayMode == null)
            {
                return Page();
            }

            _context.PayMode.Add(PayMode);
            await _context.SaveChangesAsync();

            return RedirectToPage("/PayMode/Visualizar/Index");
        }
    }
}