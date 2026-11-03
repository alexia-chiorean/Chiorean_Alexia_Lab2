using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiorean_Alexia_Lab2.Models;

namespace Chiorean_Alexia_Lab2.Pages.BorrowingPages;

public class IndexModel : PageModel
{
    private readonly Chiorean_Alexia_Lab2Context _context;

    public IndexModel(Chiorean_Alexia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Borrowing> Borrowing { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Borrowing = await _context.Borrowing
            .Include(b => b.Book)
                .ThenInclude(b => b.Author)
            .Include(b => b.Member)
            .ToListAsync();
    }
}
