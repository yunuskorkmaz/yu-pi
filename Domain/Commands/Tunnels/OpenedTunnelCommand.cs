using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using yu_pi.Domain.Entities;
using yu_pi.Domain.Enums;
using yu_pi.Hubs;
using yu_pi.Infrastructure.Context;

namespace yu_pi.Domain.Commands.Tunnels
{
    public class OpenedTunnelCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string PublicUrl { get; set; }
        public int Status { get; set; }
    }

    public class OpenedTunnelCommandHandler : IRequestHandler<OpenedTunnelCommand,int>
    {
        private readonly IServiceProvider serviceProvider;

        public OpenedTunnelCommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<int> Handle(OpenedTunnelCommand request, CancellationToken cancellationToken)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<YupiContext>();
                var tunnelHub = scope.ServiceProvider.GetService<IHubContext<TunnelHub>>();
                var currentTunnel = context.Tunnels.FirstOrDefault(a => a.Id == request.Id);
                if(currentTunnel != null){
                    currentTunnel.PublicUrl = request.PublicUrl;
                    currentTunnel.Status = request.Status;
                    var result = await context.SaveChangesAsync();
                    await tunnelHub.Clients.All.SendAsync("onTunnelUpdated",currentTunnel);
                    return result;
                }
                return 0;
            }
        }
    }
}