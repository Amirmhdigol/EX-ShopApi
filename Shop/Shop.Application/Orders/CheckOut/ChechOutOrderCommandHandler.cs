using Common.Application;
using Shop.Domain.OrderAgg;

namespace Shop.Application.Orders.CheckOut
{
    public class ChechOutOrderCommandHandler : IBaseCommandHandler<ChechOutOrderCommand>
    {
        private readonly IOrderRepository _repository;

        public ChechOutOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChechOutOrderCommand request, CancellationToken cancellationToken)
        {
            var CurrrentOrder = await _repository.GetCurrentUserOrder(request.UserId);
            if (CurrrentOrder == null)
                return OperationResult.NotFound();

            var Address = new OrderAddress(request.Provice, request.City, request.Name, request.Family, request.PostalAddress
                                                         , request.PostalCode, request.NationalCode, request.PhoneNumber);
            CurrrentOrder.Checkout(Address);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}

