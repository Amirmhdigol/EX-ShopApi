using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Sellers.Edit
{
    public class EditSellerCommandValidator : AbstractValidator<EditSellerCommand>
    {
        public EditSellerCommandValidator()
        {
            RuleFor(a => a.shopName)
                .NotEmpty().WithMessage(ValidationMessages.required("Shop name"));

            RuleFor(a => a.shopName)
                .NotEmpty().WithMessage(ValidationMessages.required("NationalCode")).ValidNationalId();
        }
    }
}
