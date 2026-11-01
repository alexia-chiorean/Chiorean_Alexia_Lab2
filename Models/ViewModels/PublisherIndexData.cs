using Chiorean_Alexia_Lab2.Models;

namespace Chiorean_Alexia_Lab2.Models.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>();
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}