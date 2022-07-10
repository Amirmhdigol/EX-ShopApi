using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users;
public static class UserMapper
{
    public static UserDTO Map(this User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            CreationDate = user.CreationDate,
            Family = user.Family,
            Gender = user.Gender,
            Name = user.Name,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber,
            UserAvatar = user.UserAvatar,
            IsActive = user.IsActive,
            RoleId = user.RoleId,
        };
    }
    //public static async Task<UserDTO> SetUserRoleTitles(this UserDTO userDTO, ShopContext _context)
    //{
    //    var roleIds = userDTO.UserRoles.RoleId;
    //    var result = await _context.Roles.FirstOrDefaultAsync(a => roleIds == a.Id);
    //    var role = new UserRoleDTO()
    //    {
    //        RoleId = result.Id,
    //        RoleTitle = result.Title
    //    };
    //    userDTO.UserRoles = role;
    //    return userDTO;
    //}
    public static UserFilterData MapFilterData(this User user)
    {
        return new UserFilterData()
        {
            Id = user.Id,
            CreationDate = user.CreationDate,
            Email = user.Email,
            Family = user.Family,
            Gender = user.Gender,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            UserAvatar = user.UserAvatar,
            RoleId = user.RoleId
        };
    }
}

