using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiorean_Alexia_Lab2.Models;

namespace Chiorean_Alexia_Lab2.Pages.MemberPages;

public class IndexModel : PageModel
{
    private readonly Chiorean_Alexia_Lab2Context _context;

    public IndexModel(Chiorean_Alexia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Chiorean_Alexia_Lab2.Models.Member> Member { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Member = await _context.Member.ToListAsync();
    }
}
