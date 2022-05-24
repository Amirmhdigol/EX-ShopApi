using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens;

public class GetUserTokenByRefreshTokenQueryHandler : IQueryHandler<GetUserTokenByRefreshTokenQuery, UserTokenDTO>
{
    private readonly DapperContext _dapper;
    public GetUserTokenByRefreshTokenQueryHandler(DapperContext context)
    {
        _dapper = context;
    }

    public async Task<UserTokenDTO> Handle(GetUserTokenByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapper.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {_dapper.UserTokens} WHERE HashedRefreshToken=@hashRefreshToken";

        return await connection.QueryFirstOrDefaultAsync<UserTokenDTO>(sql, new { hashRefreshToken = request.HashedRefreshToken});
    }
} 