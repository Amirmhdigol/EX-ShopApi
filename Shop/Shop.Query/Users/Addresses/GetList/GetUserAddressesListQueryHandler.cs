using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.Addresses.DTOs;

namespace Shop.Query.Users.Addresses.GetList;

public class GetUserAddressesListQueryHandler : IQueryHandler<GetUserAddressesListQuery, List<AddressDTO>>
{
    private readonly DapperContext _context;
    public GetUserAddressesListQueryHandler(DapperContext context)
    {
        _context = context;
    }
    ]
    public async Task<List<AddressDTO>> Handle(GetUserAddressesListQuery request, CancellationToken cancellationToken)
    {
        var sql = $"Select * from {_context.UserAddresses} where UserId=@userId";
        using var context = _context.CreateConnection();
        var result = await context.QueryAsync<AddressDTO>(sql, new { id = request.userId });
        return result.ToList();
    }
}