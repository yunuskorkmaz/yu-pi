using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using yu_pi.Domain.Entities;
using yu_pi.Infrastructure.Context;

namespace yu_pi.Features.Ngrok
{
    public class Ngrok
    {
        public class TunnelModel
        {
            public string Name { get; set; }
            public string PublicUrl { get; set; }
            public string Proto { get; set; }
            public string LocalAddress { get; set; }
        }

        public class TunnelRegisterCommand : IRequest<string>
        {
            public List<TunnelModel> Tunnels { get; set; }
        }

        public class MappingProfile : AutoMapper.Profile
        {
            public MappingProfile()
            {
                CreateMap<Tunnel, TunnelModel>().ReverseMap();
            }
        }

        public class TunnelRegisterHandler : IRequestHandler<TunnelRegisterCommand, string>
        {
            private readonly IServiceProvider serviceProvider;

            public TunnelRegisterHandler(IServiceProvider serviceProvider)
            {
                this.serviceProvider = serviceProvider;
            }
            public async Task<string> Handle(TunnelRegisterCommand request, CancellationToken cancellationToken)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<YupiContext>();
                    var mapper = scope.ServiceProvider.GetService<IMapper>();
                    var tunnels = mapper.Map<List<Tunnel>>(request.Tunnels);
                    context.Tunnels.Clear();
                    await context.Tunnels.AddRangeAsync(tunnels);
                    await context.SaveChangesAsync();
                }
                return "";
            }
        }
    }
}