using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiorean_Alexia_Lab2.Models;

namespace Chiorean_Alexia_Lab2.Pages.BookPages;

public class CreateModel : PageModel
{
    private readonly Chiorean_Alexia_Lab2Context _context;

    public CreateModel(Chiorean_Alexia_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Set<Chiorean_Alexia_Lab2.Models.Authors>(), "ID", "FirstName");
        Categories = _context.Category.ToList();
        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;
    [BindProperty]
    public List<int> SelectedCategoryIds { get; set; } = new();

    public List<Category> Categories { get; set; } = new();

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        Book.BookCategories = new List<BookCategory>();

        foreach (var categoryId in SelectedCategoryIds)
        {
            Book.BookCategories.Add(new BookCategory
            {
                CategoryID = categoryId
            });
        }
        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
