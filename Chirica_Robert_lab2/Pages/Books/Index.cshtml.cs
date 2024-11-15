using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chirica_Robert_lab2.Models;

namespace Chirica_Robert_lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Chirica_Robert_lab2.Data.Chirica_Robert_lab2Context _context;

        public IndexModel(Chirica_Robert_lab2.Data.Chirica_Robert_lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .ToListAsync();
        }
    }
}
