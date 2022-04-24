using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Products.AddImage
{
    public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
    {
        public AddProductImageCommandValidator()
        {
            RuleFor(a => a.ImageFile)
                .NotNull().WithMessage(ValidationMessages.required("Image"))
                .JustImageFile();

            RuleFor(a => a.Sequence)
                .GreaterThanOrEqualTo(0);

        }
    }
}
