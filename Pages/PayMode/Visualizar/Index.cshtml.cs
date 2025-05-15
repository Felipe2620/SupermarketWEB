using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayMode
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;
        public IndexModel(SupermarketContext context)
        {
            _context = context;
        }

        public IList<SupermarketWEB.Models.PayMode> PayMode { get; set; } = default!; 

        public async Task OnGetAsync()
        {
            if (_context.PayMode != null)
            {
                PayMode = await _context.PayMode.ToListAsync();
            }
        }
    }
}