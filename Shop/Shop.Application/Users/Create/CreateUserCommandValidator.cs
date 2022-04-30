using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(a => a.PhoneNumber).ValidPhoneNumber();
            RuleFor(a => a.Email).EmailAddress().WithMessage("Email Is not valid");

            RuleFor(f => f.Password)
                .NotEmpty().WithMessage(ValidationMessages.required("Password"))
                .NotNull().WithMessage(ValidationMessages.required("Password"))
                .MinimumLength(4).WithMessage("Password is too short");
        }
    }
}
