using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.SiteEntities.ShippingMethods.Edit;
public record EditShippingMethodCommand(long Id, string Title, int Cost) : IBaseCommand;
