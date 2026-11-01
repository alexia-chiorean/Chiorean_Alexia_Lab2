using System;
using System.Linq;
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
    public BookData BookD { get; set; } = new();
    public string TitleSort { get; set; }
    public string AuthorSort { get; set; }
    public string CurrentFilter { get; set; }

    public async Task OnGetAsync(int? authorID, string sortOrder, string searchString)
    {
        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";

        CurrentFilter = searchString;

        BookD = new BookData();

        BookD.Books = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        if (!String.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books.Where(b =>
                b.Title.Contains(searchString) ||
                b.Author.FirstName.Contains(searchString) ||
                b.Author.LastName.Contains(searchString));
        }

        if (authorID.HasValue)
        {
            BookD.Books = BookD.Books.Where(b => b.AuthorID == authorID.Value);
        }

        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books.OrderByDescending(b => b.Title);
                break;

            case "author":
                BookD.Books = BookD.Books.OrderBy(b => b.Author == null ? "" : b.Author.FullName);
                break;

            case "author_desc":
                BookD.Books = BookD.Books.OrderByDescending(b => b.Author == null ? "" : b.Author.FullName);
                break;

            default:
                BookD.Books = BookD.Books.OrderBy(b => b.Title);
                break;
        }

        Book = BookD.Books.ToList();

        AuthorList = new SelectList(
            _context.Authors,
            "ID",
            "FirstName",
            authorID
        );
    }
}
