using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.Edit
{
    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(a => a.Title)
                 .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(a => a.Slug)
                   .NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));
        }
    }
}