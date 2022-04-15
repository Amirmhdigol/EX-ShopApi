using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg.ValueObjects
{
    public class ShippingMethod
    {
        public string ShipppingType { get; private set; }
        public int ShippingCost { get; private set; }
    }
}
