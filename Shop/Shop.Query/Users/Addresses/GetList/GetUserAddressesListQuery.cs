using Common.Query;
using Shop.Query.Users.Addresses.DTOs;

namespace Shop.Query.Users.Addresses.GetList;

public record GetUserAddressesListQuery(long userId) : IQuery<List<AddressDTO>>;
