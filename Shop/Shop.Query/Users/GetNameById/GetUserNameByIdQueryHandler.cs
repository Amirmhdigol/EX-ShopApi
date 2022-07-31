using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetNameById;

public class GetUserNameByIdQueryHandler : IQueryHandler<GetUserNameByIdQuery, string>
{
    private readonly DapperContext _context;
    public GetUserNameByIdQueryHandler(DapperContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(GetUserNameByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $"select u.Name,u.Family from {_context.Users} u where Id=@Id";
        using var connection = _context.CreateConnection();
        var res = await connection.QueryFirstOrDefaultAsync<UserNameDTO>(sql, new { Id = request.UserId });
        return (res.Name + " " + res.Family);
    }
}