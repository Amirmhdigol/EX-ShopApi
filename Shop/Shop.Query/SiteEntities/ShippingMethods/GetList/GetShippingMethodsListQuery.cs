using Shop.Query.SiteEntities.ShippingMethods.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.SiteEntities.ShippingMethods.GetList;
public record GetShippingMethodsListQuery : IQuery<List<ShippingMethodDTO?>>;