using Microsoft.EntityFrameworkCore;
using Yupi.Domain.Entities;

namespace Yupi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}