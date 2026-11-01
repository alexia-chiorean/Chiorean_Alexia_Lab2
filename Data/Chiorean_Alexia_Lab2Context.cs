using Microsoft.EntityFrameworkCore;

public class Chiorean_Alexia_Lab2Context(DbContextOptions<Chiorean_Alexia_Lab2Context> options) : DbContext(options)
{
    public DbSet<Chiorean_Alexia_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Chiorean_Alexia_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Chiorean_Alexia_Lab2.Models.Authors> Authors { get; set; } = default!;
}
