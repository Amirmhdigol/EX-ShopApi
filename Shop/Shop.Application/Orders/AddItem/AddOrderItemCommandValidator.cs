using FluentValidation;

namespace Shop.Application.Orders.AddItem
{
    public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator()
        {
            RuleFor(a => a.Count).GreaterThanOrEqualTo(1).WithMessage("the count must be more than 0");
        }
    }
}
