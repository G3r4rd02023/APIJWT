using Microsoft.EntityFrameworkCore;

namespace ProyectoApi.Backend.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
    }
}