using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Orders.CheckOut
{
    public class ChechOutOrderCommandValidator : AbstractValidator<ChechOutOrderCommand>
    {
        public ChechOutOrderCommandValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage(ValidationMessages.required("Name")).NotNull();
            RuleFor(a => a.Family).NotEmpty().WithMessage(ValidationMessages.required("Family")).NotNull();
            RuleFor(a => a.City).NotEmpty().WithMessage(ValidationMessages.required("City")).NotNull();
            RuleFor(a => a.Provice).NotEmpty().WithMessage(ValidationMessages.required("Province")).NotNull();
            RuleFor(a => a.PostalAddress).NotEmpty().WithMessage(ValidationMessages.required("PostalAddress")).NotNull();
            RuleFor(a => a.PostalCode).NotEmpty().WithMessage(ValidationMessages.required("PostalCode")).NotNull();

            RuleFor(a => a.PhoneNumber).NotEmpty().WithMessage(ValidationMessages.required("Phone")).NotNull()
                .MaximumLength(11).WithMessage("PhoneNumber is Invalid")
                .MinimumLength(11).WithMessage("PhoneNumber is Invalid");

            RuleFor(a => a.NationalCode).NotEmpty().WithMessage(ValidationMessages.required("National Code")).NotNull()
                .MaximumLength(10).WithMessage("NationalCode is Invalid")
                .MinimumLength(10).WithMessage("NationalCode is Invalid")
                .ValidNationalId();

        }
    }
}
