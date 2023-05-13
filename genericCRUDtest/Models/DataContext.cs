using genericCRUDtest.Models;
using Microsoft.EntityFrameworkCore;

namespace genericCRUD.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Figure> Figures { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Laser> Lasers { get; set; }
    }
}
