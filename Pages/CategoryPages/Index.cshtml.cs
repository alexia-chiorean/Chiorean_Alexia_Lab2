using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiorean_Alexia_Lab2.Models;

namespace Chiorean_Alexia_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Chiorean_Alexia_Lab2Context _context;

    public IndexModel(Chiorean_Alexia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;
    public IList<Book> Books { get; set; } = new List<Book>();
    public int? SelectedCategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        Category = await _context.Category.ToListAsync();

        if (id.HasValue)
        {
            SelectedCategoryID = id.Value;

            Books = await _context.Book
                .Include(b => b.Author)
                .Where(b => b.BookCategories.Any(bc => bc.CategoryID == id.Value))
                .ToListAsync();
        }
    }
}
