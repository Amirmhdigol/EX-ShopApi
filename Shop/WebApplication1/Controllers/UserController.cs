using AutoMapper;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;
using Shop.Query.Roles.DTOs;
using Shop.Query.Users.DTOs;

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
    public async Task<ApiResult> CreateUser([FromForm] CreateUserCommand command)
    {
        return CommandResult(await _facade.CreateUser(command));
    }

    [HttpPut("Current")]
    public async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
    {
        var commandModel = new EditUserCommand(User.GetUserId(), command.Avatar, command.Name, command.Family
            , command.Email, command.Gender, command.PhoneNumber);

        var result = await _facade.EditUser(commandModel);
        return CommandResult(result);
    }

    [HttpPut]
    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    public async Task<ApiResult> EditUser(EditUserCommand command)
    {
        command.UserId = User.GetUserId();
        return CommandResult(await _facade.EditUser(command));
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
    [HttpGet("{userId}")]
    public async Task<ApiResult<UserDTO?>> GetUserById(long userId)
    {
        return QueryResult(await _facade.GetUserById(userId));
    }

    [PermissionChecker(Domain.RoleAgg.Permission.User_Management)]
    [HttpGet("{phoneNumber}")]
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

    [HttpGet("GetRole")]
    public async Task<ApiResult<List<RoleDTO>>> GetUserRoles(long userId)
    {
        var result = await _facade.GetUsersRoleById(userId);
        return QueryResult(result);
    }
}