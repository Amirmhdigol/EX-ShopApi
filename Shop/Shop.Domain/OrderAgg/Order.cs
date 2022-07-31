using Common.Domain.Bases;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg
{
    public class Order : BaseAggregate
    {
        private Order()
        {

        }
        public Order(long userId)
        {
            UserId = userId;
            Status = OrderStatus.pennding;
            Items = new List<OrderItem>();
        }

        public long UserId { get; set; }
        public OrderStatus Status { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public OrderDiscount? Discount { get; private set; }
        public OrderAddress? Address { get; private set; }
        public OrderShippingMethod? ShippingMethod { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public int TotalPrice
        {
            get
            {
                var totalPrice = Items.Sum(a => a.TotalPrice);

                if (ShippingMethod != null)
                    totalPrice += ShippingMethod.ShippingCost;

                if (Discount != null)
                    totalPrice -= Discount.DiscountAmount;

                return totalPrice;
            }
        }
        public int ItemCount => Items.Count;

        public void AddItem(OrderItem item)
        {
            ChangeOrderGuard();

            var OldItem = Items.FirstOrDefault(a => a.Id == item.InventoryId);
            if (OldItem != null)
            {
                OldItem.ChangeCount(item.Count + OldItem.Count);
                return;
            }

            Items.Add(item);
        }
        public void RemoveItem(long itemId)
        {
            ChangeOrderGuard();

            var CurrentItem = Items.FirstOrDefault(a => a.Id == itemId);
            if (CurrentItem != null)
                Items.Remove(CurrentItem);
        }
        public void IncreaseItemCount(long itemId, int count)
        {
            ChangeOrderGuard();

            var Currentitem = Items.FirstOrDefault(a => a.Id == itemId);
            if (Currentitem == null)
                throw new NullOrEmptyDomainDataException();

            Currentitem.IncreaseCount(count);
        }
        public void DecreaseItemCount(long itemId, int count)
        {
            ChangeOrderGuard();

            var Currentitem = Items.FirstOrDefault(a => a.Id == itemId);
            if (Currentitem == null)
                throw new NullOrEmptyDomainDataException();

            Currentitem.DecreaseCount(count);
        }
        public void ChangeItemCount(long itemId, int newCount)
        {
            ChangeOrderGuard();
            var CurrentItem = Items.FirstOrDefault(a => a.Id == itemId);
            if (CurrentItem == null)
                throw new NullOrEmptyDomainDataException();

            CurrentItem.ChangeCount(newCount);
        }
        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
            LastUpdate = DateTime.Now;
        }
        public void Checkout(OrderAddress orderAddress)
        {
            ChangeOrderGuard();
            Address = orderAddress;
        }
        public void ChangeOrderGuard()
        {
            if (Status != OrderStatus.pennding)
                throw new InvalidDomainDataException("Cant edit this Order");
        }
    }
}
