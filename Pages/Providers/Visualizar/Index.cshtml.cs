using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Providers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;
        public IndexModel(SupermarketContext context)
        {
            _context = context;
        }

        public IList<SupermarketWEB.Models.Providers> Providers { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Providers != null)
            {
                Providers = await _context.Providers.ToListAsync();
            }
        }
    }
}