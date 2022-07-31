using Common.Domain.Bases;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SiteEntities;
public class ShippingMethod : BaseEntity
{
    public ShippingMethod(string title, int cost)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        Cost = cost;
    }
    public void Edit(string title, int cost)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        Cost = cost;
    }
    public string Title { get; set; }
    public int Cost { get; set; }
}