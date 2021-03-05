using MediatR;
using yu_pi.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using yu_pi.Hubs;
using yu_pi.Infrastructure.Context;
using AutoMapper;

namespace yu_pi.Domain.Commands.Tunnels
{
    public class CreateTunnelCommand : Tunnel, IRequest<Tunnel>
    {
        
    }

    public class CreateTunnelCommandHandler : IRequestHandler<CreateTunnelCommand, Tunnel>
    {
        private readonly IServiceProvider serviceProvider;

        public CreateTunnelCommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task<Tunnel> Handle(CreateTunnelCommand request, CancellationToken cancellationToken)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                
                var context = scope.ServiceProvider.GetService<YupiContext>();
                var mapper = scope.ServiceProvider.GetService<IMapper>();
                request.Status = "preparing";
                request.publicUrl = "";
                await context.Tunnels.AddAsync(request);
                var result = await context.SaveChangesAsync();
                if (Convert.ToBoolean(result))
                {
                    var tunnel = mapper.Map<Tunnel>(request);
                    return tunnel;
                }
                return null;
            }
        }
    }
}