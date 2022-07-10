using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Shop.Query.Users.GetUserRoleId;

public class GetUserRoleIdQueryHandler : IQueryHandler<GetUserRoleIdQuery, LlongRoleId>
{
    private readonly ShopContext context;
    public GetUserRoleIdQueryHandler(ShopContext context)
    {
        this.context = context;
    }

    public async Task<LlongRoleId> Handle(GetUserRoleIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId, cancellationToken);
        if (user == null) return null;

        var userRoleId = user.RoleId;/* UserRoles.UserId == request.UserId ? user.UserRoles.RoleId : throw new NullOrEmptyDomainDataException();*/

        return new LlongRoleId
        {
            RoleId = userRoleId,
        };
    }
}