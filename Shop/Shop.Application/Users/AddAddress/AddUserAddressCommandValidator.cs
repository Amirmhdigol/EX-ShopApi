using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.AddAddress
{
    public class AddUserAddressCommandValidator : AbstractValidator<AddUserAddressCommand>
    {
        public AddUserAddressCommandValidator()
        {
            RuleFor(c => c.City).NotEmpty().WithMessage(ValidationMessages.required("Enter City"));
            RuleFor(c => c.Province).NotEmpty().WithMessage(ValidationMessages.required("Enter Province"));
            RuleFor(c => c.Name).NotEmpty().WithMessage(ValidationMessages.required("Enter name"));
            RuleFor(c => c.Family).NotEmpty().WithMessage(ValidationMessages.required("Enter Family"));
            RuleFor(c => c.NationalCode).NotEmpty().WithMessage(ValidationMessages.required("Enter National code")).ValidNationalId();
            RuleFor(c => c.PostalAddress).NotEmpty().WithMessage(ValidationMessages.required("Enter Postal Address"));
            RuleFor(c => c.PostalCode).NotEmpty().WithMessage(ValidationMessages.required("Enter Postal Code"));
        }
    }

}
