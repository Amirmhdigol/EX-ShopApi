using Shop.Query.SiteEntities.ShippingMethods.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.SiteEntities.ShippingMethods.GetById;
public record GetShippingMethodByIdQuery(long Id) : IQuery<ShippingMethodDTO?>;
    