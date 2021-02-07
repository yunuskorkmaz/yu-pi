using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using yu_pi.Helpers;
using yu_pi.Infrastructure.Context;
using yu_pi.Infrastructure.Errors;

namespace yu_pi.Features.User
{
    public class Login
    {
        public class LoginCommand : IRequest<LoginResult>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginResult
        {
            public string Token { get; set; }
        }

        public class LoginCommandValidator : AbstractValidator<LoginCommand>
        {
            public LoginCommandValidator()
            {
                RuleFor(x => x.Email).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty();
            }
        }

        public class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
        {
            private readonly IServiceProvider serviceProvider;

            public LoginHandler(IServiceProvider serviceProvider)
            {
                this.serviceProvider = serviceProvider;
            }
            public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
            {

                using (var scope = serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<YupiContext>();
                    var configuration = scope.ServiceProvider.GetService<IConfiguration>();
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);
                    if (user is null)
                    {
                        throw new RestException(HttpStatusCode.Unauthorized, new
                        {
                            User = "user not found"
                        });
                    }

                    var token = new JwtTokenGenerator(configuration).CreateAccessToken(user);
                    return new LoginResult
                    {
                        Token = token
                    };
                }

            }
        }
    }
}