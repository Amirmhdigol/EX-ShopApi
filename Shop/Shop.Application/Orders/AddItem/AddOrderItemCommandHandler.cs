using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Orders.AddItem
{
    public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly ISellerRepository _sellerRepository;
        public AddOrderItemCommandHandler(IOrderRepository repository, ISellerRepository sellerRepository)
        {
            _repository = repository;
            _sellerRepository = sellerRepository;
        }

        public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            var Inventory = await _sellerRepository.GetInventoryById(request.InventoryId);
            if (Inventory == null)
                return OperationResult.NotFound();

            if (Inventory.Count < request.Count)
                return OperationResult.Error("Not Enough goods");

            var UserOrder = await _repository.GetCurrentUserOrder(request.UserId);

            if (UserOrder == null)
            {
                UserOrder = new Order(request.UserId);
                UserOrder.AddItem(new OrderItem(request.InventoryId, request.Count, Inventory.Price));
                _repository.Add(UserOrder);
            }
            else
            {
                UserOrder.AddItem(new OrderItem(request.InventoryId, request.Count, Inventory.Price));
                if (ItemCountBiggerThanInventoryCount(Inventory, UserOrder))
                {
                    return OperationResult.Error("");
                }
            }
            await _repository.Save();
            return OperationResult.Success();
        }
        private bool ItemCountBiggerThanInventoryCount(InventoryResult inventory, Order order)
        {
            var OrderItem = order.Items.First(f => f.InventoryId == inventory.Id);
            if (OrderItem.Count > inventory.Count)
                return true;

            return false;
        }
    }
}
