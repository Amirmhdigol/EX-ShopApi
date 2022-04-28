using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Banners.Create
{
    public class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommand>
    {
        public CreateBannerCommandValidator()
        {
            RuleFor(a => a.ImageFile).NotNull().WithMessage(ValidationMessages.required("Image")).JustImageFile();
            RuleFor(a => a.Link).NotNull().NotEmpty().WithMessage(ValidationMessages.required("link"));
        }
    }
}
