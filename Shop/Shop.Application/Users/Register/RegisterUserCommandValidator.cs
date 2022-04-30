using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Users.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(f => f.Password)
               .NotEmpty().WithMessage(ValidationMessages.required("Password"))
               .NotNull().WithMessage(ValidationMessages.required("Password"))
               .MinimumLength(4).WithMessage("Password is too short");
        }
    }
}
