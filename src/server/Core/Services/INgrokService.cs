using System.Threading.Tasks;
using Core.Dtos.Ngrok;
using Core.Entities;

namespace Core.Services
{
    public interface INgrokService
    {
         Task<bool> Update(Tunnel model);
    }
}