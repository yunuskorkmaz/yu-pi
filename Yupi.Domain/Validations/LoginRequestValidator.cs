using FluentValidation;
using Yupi.Domain.Dtos.Auth;

namespace Yupi.Domain.Validations
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("field.mustBeNotEmpty").EmailAddress().WithMessage("login.mustMailFormat");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("login.mustBeMinChar8");
        }
    }
}