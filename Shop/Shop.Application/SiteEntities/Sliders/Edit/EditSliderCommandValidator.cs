using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Sliders.Edit
{
    public class EditSliderCommandValidator : AbstractValidator<EditSliderCommand>
    {
        public EditSliderCommandValidator()
        {
            RuleFor(a => a.ImageFile).JustImageFile();
            RuleFor(a => a.Link).NotNull().NotEmpty().WithMessage(ValidationMessages.required("link"));
            RuleFor(a => a.Title).NotNull().NotEmpty().WithMessage(ValidationMessages.required("Title"));
        }
    }

}
