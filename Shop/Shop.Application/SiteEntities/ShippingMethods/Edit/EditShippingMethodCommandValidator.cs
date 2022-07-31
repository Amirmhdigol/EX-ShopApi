using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.SiteEntities.ShippingMethods.Edit;

public class EditShippingMethodCommandValidator : AbstractValidator<EditShippingMethodCommand>
{
    public EditShippingMethodCommandValidator()
    {
        RuleFor(a => a.Cost).NotNull().NotEmpty().WithMessage(ValidationMessages.required("Cost"));
        RuleFor(a => a.Title).NotNull().NotEmpty().WithMessage(ValidationMessages.required("Title"));
    }
}
