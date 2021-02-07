using Microsoft.EntityFrameworkCore;
using yu_pi.Domain.Entities;

namespace yu_pi.Infrastructure.Context
{
    public class YupiContext : DbContext{

        public YupiContext(DbContextOptions options):base(options)
        {    
        }

        public DbSet<User> Users { get;set; }
        public DbSet<Tunnel> Tunnels { get;set; }
    }
}