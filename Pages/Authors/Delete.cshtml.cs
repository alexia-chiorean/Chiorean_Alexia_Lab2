using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiorean_Alexia_Lab2.Models;

namespace Chiorean_Alexia_Lab2.Pages.AuthorsPages;

public class DeleteModel : PageModel
{
    private readonly Chiorean_Alexia_Lab2Context _context;

    public DeleteModel(Chiorean_Alexia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Chiorean_Alexia_Lab2.Models.Authors Authors { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var authors = await _context.Authors.FirstOrDefaultAsync(m => m.ID == id);
        if (authors is null)
        {
            return NotFound();
        }
        else
        {
            Authors = authors;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var authors = await _context.Authors.FindAsync(id);
        if (authors != null)
        {
            Authors = authors;
            _context.Authors.Remove(Authors);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
