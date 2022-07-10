using Shop.Domain.RoleAgg.Repository;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Query.Users.GetUserRoleId;
public record GetUserRoleIdQuery(long UserId) : IQuery<LlongRoleId>;
public class LlongRoleId
{
    public long RoleId { get; set; }
}
