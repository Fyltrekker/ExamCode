using Microsoft.EntityFrameworkCore;
using ViseoTechnicalTest.Database.DatabasModels;

namespace ViseoTechnicalTest.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Users>(new UsersMap());
        }
    }
}
