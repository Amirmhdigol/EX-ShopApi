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
            UserRoles = user.UserRoles.Select(a => new UserRoleDTO()
            {
                Id = a.Id,
                CreationDate = a.CreationDate,
                RoleId = a.RoleId,
                RoleTitle = ""
            }).ToList()
        };
    }
    public static async Task<UserDTO> SetUserRoleTitles(this UserDTO userDTO, ShopContext _context)
    {
        var roleIds = userDTO.UserRoles.Select(a => a.RoleId);
        var result = await _context.Roles.Where(a => roleIds.Contains(a.Id)).ToListAsync();
        var roles = new List<UserRoleDTO>();
        foreach (var role in result)
        {
            roles.Add(new UserRoleDTO()
            {
                RoleId = role.Id,
                RoleTitle = role.Title,
            });
        };
        userDTO.UserRoles = roles;
        return userDTO;
    }
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
        };
    }
}

