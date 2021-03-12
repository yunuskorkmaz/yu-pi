using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using yu_pi.Infrastructure.Context;
using yu_pi.Services;

namespace yu_pi.Domain.Commands.Tunnels
{
    public class DeleteTunnelCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteTunnelCommandHandler : IRequestHandler<DeleteTunnelCommand,bool>
    {
        private readonly IServiceProvider serviceProvider;
        public DeleteTunnelCommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task<bool> Handle(DeleteTunnelCommand request, CancellationToken cancellationToken)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<YupiContext>();
                var ably = scope.ServiceProvider.GetService<AblyClientService>();
                var tunnel = context.Tunnels.FirstOrDefault(a => a.Id == request.Id);
                if(tunnel != null){
                    context.Tunnels.Remove(tunnel);
                }
                var result = await context.SaveChangesAsync();
                await ably.Client.Channels.Get("tunnels").PublishAsync("tunnelDeleted",JsonConvert.SerializeObject(tunnel));
                Console.WriteLine("deleted resılt " + result.ToString());
                return Convert.ToBoolean(result);
            }
        }
    }
}