using AutoMapper;
using yu_pi.Domain.Commands.Tunnels;
using yu_pi.Domain.Entities;

namespace yu_pi.Domain.Mappers
{
    public class YupiMapper : Profile
    {
        public YupiMapper()
        {
            CreateMap<Tunnel,CreateTunnelCommand>().ReverseMap();
        }
    }
}