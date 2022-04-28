using Common.Application;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.AddInventory
{
    public class AddSellerInventoryCommand : IBaseCommand
    {
        public AddSellerInventoryCommand(long sellerId, long productId, int count, int price, int discountPercentage)
        {
            SellerId = sellerId;
            ProductId = productId;
            Count = count;
            Price = price;
            DiscountPercentage = discountPercentage;
        }
        public long SellerId { get; private set; }
        public long ProductId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public int DiscountPercentage { get; set; }
    }
    public class AddSellerInventoryCommandValidator : AbstractValidator<AddSellerInventoryCommand>
    {
        public AddSellerInventoryCommandValidator()
        {

        }
    }
}
