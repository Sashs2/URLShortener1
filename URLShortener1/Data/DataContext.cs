using Microsoft.EntityFrameworkCore;
using URLShortener1.Entities;

namespace URLShortener1.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base (options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Url> Urls { get; set; }


    }
}
