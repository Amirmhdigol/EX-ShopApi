using Dapper;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Orders;
internal static class OrderMapper
{
    public static OrderDto Map(this Order order)
    {
        return new OrderDto()
        {
            CreationDate = order.CreationDate,
            Id = order.Id,
            Status = order.Status,
            Address = order.Address,
            Discount = order.Discount,
            Items = new(),
            LastUpdate = order.LastUpdate,
            ShippingMethod = order.ShippingMethod,
            UserFullName = "",
            UserId = order.UserId,
        };
    }
    public static async Task<List<OrderItemDto>> GetOrderItems(this OrderDto orderDTO, DapperContext dapperContext)
    {
        using var connection = dapperContext.CreateConnection();
        var sql = $"SELECT s.ShopName ,o.OrderId ,o.InventoryId ,o.Count ,o.Price ," +
            $"p.Title as ProductTitle ,p.Slug as ProductSlug ,p.ImageName as ProductImageName " +
            $"FROM {dapperContext.OrderItems} o " +
            $"INNER JOIN {dapperContext.Inventories} i on o.InventoryId=i.Id " +
            $"INNER JOIN {dapperContext.Products} p on i.ProductId=p.id " +
            $"INNER JOIN {dapperContext.Sellers} s on i.SellerId=s.id " +
            $"WHERE o.OrderId=@orderId ";

        var result = await connection.QueryAsync<OrderItemDto>(sql, new { orderId = orderDTO.Id });
        return result.ToList()  ;
    }
    public static OrderFilterData MapFilterData(this Order order, ShopContext context)
    {
        var UserName = context.Users.Where(a => a.Id == order.UserId)
            .Select(user => $"{user.Name} {user.Family}").FirstOrDefault();

        return new OrderFilterData()
        {
            Status = order.Status,
            City = order.Address?.City,
            ShippingType = order.ShippingMethod?.ShipppingType,
            CreationDate = order.CreationDate,
            Id = order.Id,
            Provice = order.Address?.Provice,
            TotalItemCount = order.ItemCount,
            UserFullName = UserName,
            UserId = order.UserId,
            TotalPrice = order.TotalPrice,
        };
    }
}

