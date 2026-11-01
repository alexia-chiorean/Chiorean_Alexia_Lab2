using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiorean_Alexia_Lab2.Models;

namespace Chiorean_Alexia_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Chiorean_Alexia_Lab2Context _context;

    public IndexModel(Chiorean_Alexia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;
    public SelectList? AuthorList { get; set; }

    public async Task OnGetAsync(int? authorID)
    {
        var books = _context.Book
     .Include(b => b.Publisher)
     .Include(b => b.Author);

        Book = authorID.HasValue
            ? await books.Where(b => b.AuthorID == authorID).ToListAsync()
            : await books.ToListAsync();

        AuthorList = new SelectList(
            _context.Authors,
            "ID",
            "FirstName",
            authorID
        );
    }
}
