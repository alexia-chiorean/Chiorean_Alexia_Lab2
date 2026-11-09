using Chiorean_Alexia_Lab2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Chiorean_Alexia_Lab2.Pages.BookPages;

[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly Chiorean_Alexia_Lab2Context _context;

    public EditModel(Chiorean_Alexia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;
    [BindProperty]
    public List<int> SelectedCategoryIds { get; set; } = new();
    public List<Category> Categories { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        SelectedCategoryIds = await _context.BookCategory
    .Where(bc => bc.BookID == Book.ID)
    .Select(bc => bc.CategoryID)
    .ToListAsync();
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Set<Chiorean_Alexia_Lab2.Models.Authors>(), "ID", "FirstName", Book.AuthorID);
        Categories = await _context.Category.ToListAsync();
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Categories = await _context.Category.ToListAsync();
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Set<Chiorean_Alexia_Lab2.Models.Authors>(), "ID", "FirstName", Book.AuthorID);
            return Page();
        }

        _context.Attach(Book).State = EntityState.Modified;
        var existingBookCategories = await _context.BookCategory
    .Where(bc => bc.BookID == Book.ID)
    .ToListAsync();

        _context.BookCategory.RemoveRange(existingBookCategories);

        foreach (var categoryId in SelectedCategoryIds)
        {
            _context.BookCategory.Add(new BookCategory
            {
                BookID = Book.ID,
                CategoryID = categoryId
            });
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(Book.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}
