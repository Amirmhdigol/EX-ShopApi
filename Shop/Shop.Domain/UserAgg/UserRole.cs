using Common.Domain.Bases;

namespace Shop.Domain.UserAgg;
public class UserRole : BaseEntity
{
    public UserRole()
    {

    }
    public UserRole(long userId, long roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
    public long UserId { get; set; }
    public long RoleId { get; set; }

    public void SetUserRole(UserRole userRole)
    {
        new UserRole(userRole.UserId, userRole.RoleId);
    }
}