using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.AddChild
{
    public class AddCategoryChildCommandValidator : AbstractValidator<AddCategoryChildCommand>
    {
        public AddCategoryChildCommandValidator()
        {
            RuleFor(a => a.Title)
                .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(a => a.Slug)
                   .NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));
        }
    }
}
