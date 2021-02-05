using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace yu_pi.Infrastructure.Context
{
    public class YupiContext : DbContext{

        public YupiContext(DbContextOptions options):base(options)
        {    
        }
    }
}