using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chiorean_Alexia_Lab2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiorean_Alexia_Lab2.Models;

namespace Chiorean_Alexia_Lab2.Pages_Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Chiorean_Alexia_Lab2Context _context;

        public IndexModel(Chiorean_Alexia_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get;set; } = default!;
        public PublisherIndexData PublisherData { get; set; } = new();
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData();

            PublisherData.Publishers = await _context.Publisher
                .Include(i => i.Books)
                    .ThenInclude(b => b.Author)
                .OrderBy(i => i.PublisherName)
                .ToListAsync();

            if (id != null)
            {
                PublisherID = id.Value;

                Publisher publisher = PublisherData.Publishers
                    .Where(i => i.ID == id.Value)
                    .Single();

                PublisherData.Books = publisher.Books;
            }
        }
    }
}
