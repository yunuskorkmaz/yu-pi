using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class EntityExtension
    {
        public static void Clear<T>(this DbSet<T> dbset) where T : class
        {
            dbset.RemoveRange(dbset);
        }
    }
}