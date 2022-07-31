using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.SiteEntities.ShippingMethods.Delete;
public record DeleteShippingMethodCommand(long Id) : IBaseCommand;
