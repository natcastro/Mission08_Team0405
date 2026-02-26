using Microsoft.EntityFrameworkCore;

namespace WebApplication8.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
    }
}