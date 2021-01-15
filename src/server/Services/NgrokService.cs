using System;
using System.Threading.Tasks;
using Core.Dtos.Ngrok;
using Core.Entities;
using Core.Exceptions;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Extensions;

namespace Services
{
    public class NgrokService : INgrokService
    {
        private readonly AppDbContext context;
        public NgrokService(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<bool> Update(Tunnel model)
        {
            try
            {
                context.Tunnels.Clear();
                await context.AddAsync(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new ApiException("ngrok.tunnelUpdateError");
            }
            
        }
    }
}