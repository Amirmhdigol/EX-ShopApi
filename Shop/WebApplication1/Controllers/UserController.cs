using AutoMapper;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.EditCurrent;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveUser;
using Shop.Application.Users.SetUserRole;
using Shop.Domain.UserAgg;
using Shop.Presentation.Facade.Users;
using Shop.Query.Roles.DTOs;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetUserRoleId;

namespace Shop.Api.Controllers;

[Authorize]
public class UserController : ApiController
{
    private readonly IUserFacade _facade;
    private readonly IMapper _mapper;

    public UserController(IUserFacade facade, IMapper mapper)
    {
        _facade = facade;
        _mapper = mapper;
    }

    [HttpPost]
    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    public async Task<ApiResult> CreateUser(CreateUserCommand command)
    {
        return CommandResult(await _facade.CreateUser(command));
    }

    [HttpPut("Current")]
    public async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
    {
        var commandModel = new EditCurrentUserCommand
        {
            UserId = User.GetUserId(),
            Avatar = command.Avatar,
            Email = command.Email,
            Family = command.Family,
            Gender = command.Gender,
            Name = command.Name,
            PhoneNumber = command.PhoneNumber,
        };
        var result = await _facade.EditCurrentUser(commandModel);
        return CommandResult(result);
    }

    [HttpPut]
    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    public async Task<ApiResult> EditUser([FromForm] EditUserCommand command)
    {
        //command.UserId = User.GetUserId();
        return CommandResult(await _facade.EditUser(command));
    }

    [HttpDelete("{userId}")]
    [AllowAnonymous]
    public async Task<ApiResult> SoftDeleteUser(long userId)
    {
        return CommandResult(await _facade.SoftDeleteUser(userId));
    }

    [HttpPut("ChangePassword")]
    public async Task<ApiResult> ChangePassword(ChangePasswordViewModel command)
    {
        var changePasswordModel = _mapper.Map<ChangePasswordCommand>(command);
        changePasswordModel.UserId = User.GetUserId();
        var result = await _facade.ChangeUserPassword(changePasswordModel);
        return CommandResult(result);
    }

    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    [HttpPost("SetRole")]
    public async Task<ApiResult> SetRole(SetUserRoleCommand command)
    {
        var result = await _facade.SetUserRole(command);
        return CommandResult(result);
    }

    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    [HttpGet("{userId}")]
    public async Task<ApiResult<UserDTO?>> GetUserById(long userId)
    {
        return QueryResult(await _facade.GetUserById(userId));
    }
    
    [HttpGet("UserName/{userId}")]
    public async Task<ApiResult<string>> GetUserNameById(long userId)
    {
        return QueryResult(await _facade.GetUserNameById(userId));
    }

    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    [HttpGet("PhoneNumber/{phoneNumber}")]
    public async Task<ApiResult<UserDTO?>> GetUserByPhoneNumber(string phoneNumber)
    {
        return QueryResult(await _facade.GetUserByPhoneNumber(phoneNumber));
    }

    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    [HttpGet]
    public async Task<ApiResult<UserFilterResult>> GetUsersByFilter([FromQuery] UserFilterParams filterParams)
    {
        return QueryResult(await _facade.GetUserByFilter(filterParams));
    }

    [HttpGet("Current")]
    public async Task<ApiResult<UserDTO>> GetCurrentUser()
    {
        var result = await _facade.GetUserById(User.GetUserId());
        return QueryResult(result);
    }

    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    [HttpGet("UserRole/{userIdRole}")]
    public async Task<ApiResult<List<RoleDTO>>> GetUserRoles(long userIdRole)
    {
        var result = await _facade.GetUsersRoleById(userIdRole);
        return QueryResult(result);
    }

    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    [HttpGet("UserRole/Id/{userRoleId}")]
    public async Task<ApiResult<LlongRoleId>> GetUserRoleId(long userId)
    {
        var result = await _facade.GetUserRoleId(userId);
        return QueryResult(result);
    }
}