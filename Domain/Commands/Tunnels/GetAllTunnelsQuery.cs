using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using yu_pi.Domain.Entities;
using yu_pi.Infrastructure.Context;

namespace yu_pi.Domain.Commands.Tunnels
{
    public class GetAllTunnelsQuery : IRequest<IEnumerable<Tunnel>>
    {

    }

    public class GetAllTunnelsQueryHandler : IRequestHandler<GetAllTunnelsQuery, IEnumerable<Tunnel>>
    {
        private readonly IServiceProvider serviceProvider;

        public GetAllTunnelsQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public Task<IEnumerable<Tunnel>> Handle(GetAllTunnelsQuery request, CancellationToken cancellationToken)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<YupiContext>();
                var tunnels = context.Tunnels.ToList();
                return Task.FromResult(tunnels.AsEnumerable());

            }
        }
    }
}