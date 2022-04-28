using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Sliders.Create
{
    public class CreateSliderCommandValidator : AbstractValidator<CreateSliderCommand>
    {
        public CreateSliderCommandValidator()
        {
            RuleFor(a => a.ImageFile).NotNull().WithMessage(ValidationMessages.required("Image")).JustImageFile();
            RuleFor(a => a.Link).NotNull().NotEmpty().WithMessage(ValidationMessages.required("link"));
            RuleFor(a => a.Title).NotNull().NotEmpty().WithMessage(ValidationMessages.required("Title"));
        }
    }
}
