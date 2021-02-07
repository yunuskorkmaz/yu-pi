using Microsoft.EntityFrameworkCore;

namespace yu_pi.Infrastructure.Context
{
    public static class YupiContextExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}