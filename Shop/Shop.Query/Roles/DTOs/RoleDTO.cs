using Common.Query;
using Shop.Domain.RoleAgg;

namespace Shop.Query.Roles.DTOs;

public class RoleDTO : BaseDTO
{
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }   
}
