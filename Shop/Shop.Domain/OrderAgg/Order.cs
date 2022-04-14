using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg
{
    public class Order
    {
        public long UserId { get; set; }
        public OrderStatus Status { get; private set; }
        public List<OrderItem> Items { get; private set; }
    } 
}
