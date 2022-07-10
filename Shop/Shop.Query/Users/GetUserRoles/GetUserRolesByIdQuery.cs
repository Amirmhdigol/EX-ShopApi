using Shop.Query.Roles.DTOs;

namespace Shop.Query.Users.GetUserRoles;
public record GetUserRolesByIdQuery(long UserId) : IQuery<List<RoleDTO>>;
