using FluentValidation;

namespace Shop.Application.Orders.IncreaseCount
{
    public class IncreaseOrderItemCountCommandValidator : AbstractValidator<IncreaseOrderItemCountCommand>
    {
        public IncreaseOrderItemCountCommandValidator()
        {
            RuleFor(f => f.Count)
              .GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از 0 باشد");
        }
    }
}
