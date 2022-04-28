using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Sellers.Create
{
    public class CreateSellerCommandValidator : AbstractValidator<CreateSellerCommand>
    {
        public CreateSellerCommandValidator()
        {
            RuleFor(a => a.ShopName)
                .NotEmpty().WithMessage(ValidationMessages.required("Shop name")); 
            
            RuleFor(a => a.ShopName)
                .NotEmpty().WithMessage(ValidationMessages.required("NationalCode")).ValidNationalId();
        }
    }
}
