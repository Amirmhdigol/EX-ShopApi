using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.Addresses.DTOs;

namespace Shop.Query.Users.Addresses.GetById;

public class GetUserAddressByIdQueryHandler : IQueryHandler<GetUserAddressByIdQuery, AddressDTO?>
{
    private readonly DapperContext _dapperContext;
    public GetUserAddressByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<AddressDTO?> Handle(GetUserAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $"Select top 1 * from {_dapperContext.UserAddresses} where id=@id";
        using var context = _dapperContext.CreateConnection();
        return await context.QueryFirstOrDefaultAsync<AddressDTO>(sql, new { id = request.AddressId });
    }
}