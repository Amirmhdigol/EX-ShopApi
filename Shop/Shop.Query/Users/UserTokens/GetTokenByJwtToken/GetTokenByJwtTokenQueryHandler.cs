using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;
namespace Shop.Query.Users.UserTokens.GetTokenByJwtToken;

public class GetTokenByJwtTokenQueryHandler : IQueryHandler<GetTokenByJwtTokenQuery, UserTokenDTO>
{
    private readonly DapperContext _dapper;
    public GetTokenByJwtTokenQueryHandler(DapperContext context)
    {
        _dapper = context;
    }

    public async Task<UserTokenDTO> Handle(GetTokenByJwtTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapper.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {_dapper.UserTokens} WHERE HashedRefreshToken=@hashJwtToken";

        return await connection.QueryFirstOrDefaultAsync<UserTokenDTO>(sql, new { hashJwtToken = request.HashJwtToken });
    }
}