using System.Collections.Generic;

namespace Chiorean_Alexia_Lab2.Models
{
    public class BookData
    {
        public IEnumerable<Book> Books { get; set; } = new List<Book>();

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
    }
}