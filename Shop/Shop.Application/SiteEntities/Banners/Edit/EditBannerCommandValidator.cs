using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Banners.Edit
{
    public class EditBannerCommandValidator : AbstractValidator<EditBannerCommand>
    {
        public EditBannerCommandValidator()
        {
            RuleFor(a => a.ImageFile).NotNull().JustImageFile();
            RuleFor(a => a.Link).NotNull().NotEmpty().WithMessage(ValidationMessages.required("link"));
        }
    }
}
