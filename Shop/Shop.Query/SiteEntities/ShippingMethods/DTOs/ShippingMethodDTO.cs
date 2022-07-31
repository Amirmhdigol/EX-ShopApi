using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.SiteEntities.ShippingMethods.DTOs;
public class ShippingMethodDTO : BaseDTO
{
    public string Title { get; set; }
    public int Cost { get; set; }
}