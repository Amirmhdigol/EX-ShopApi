using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.Edit
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(a => a.PhoneNumber).ValidPhoneNumber();
            RuleFor(a => a.Email).EmailAddress().WithMessage("Email Is not valid");

            RuleFor(f => f.Password)
                .MinimumLength(4).WithMessage("Password is too short");

            RuleFor(i => i.Avatar).JustImageFile();
        }
    }

}
