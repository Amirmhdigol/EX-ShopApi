using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.Create
{
    public partial class CreateCategoryCommandHandler
    {
        public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
        {
            public CreateCategoryCommandValidator()
            {
                RuleFor(a => a.Title)
                    .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
                
                RuleFor(a => a.Slug)
                    .NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));
            }
        }
    }
}