using Common.Query;
using Common.Query.Filter;
using Shop.Domain.UserAgg.Enums;
namespace Shop.Query.Users.DTOs;

public class UserDTO : BaseDTO
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string UserAvatar { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; }
    public string PhoneNumber { get; set; }
    public long RoleId { get; set; }
}
public class UserRoleDTO 
{
    public string RoleTitle { get; set; }
    public long RoleId { get; set; }
}
public class UserFilterData : BaseDTO
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string? Email { get; set; }
    public string UserAvatar { get; set; }
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; }
    public long RoleId { get; set; }
}
public class UserFilterParams : BaseFilterParam
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public long? Id { get; set; }
}
public class UserFilterResult : BaseFilter<UserFilterData, UserFilterParams>
{

}