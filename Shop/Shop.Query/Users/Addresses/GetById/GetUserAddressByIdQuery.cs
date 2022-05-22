using Common.Query;
using Shop.Domain.UserAgg.Repository;
using Shop.Query.Users.Addresses.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Users.Addresses.GetById;
public record GetUserAddressByIdQuery(long AddressId) : IQuery<AddressDTO?>;
