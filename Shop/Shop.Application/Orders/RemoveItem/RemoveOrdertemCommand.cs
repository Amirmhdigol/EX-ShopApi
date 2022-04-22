using Common.Application;
namespace Shop.Application.Orders.RemoveItem;

public record RemoveOrdertemCommand(long ItemId, long UserId) : IBaseCommand;

