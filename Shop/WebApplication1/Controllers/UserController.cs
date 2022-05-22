using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers;
public class UserController : ApiController
{
    private readonly IUserFacade _facade;

    public UserController(IUserFacade facade)
    {
        _facade = facade;
    }

    [HttpPost]
    public async Task<ApiResult> CreateUser([FromForm] CreateUserCommand command)
    {
        return CommandResult(await _facade.CreateUser(command));
    }

    [HttpPut]
    public async Task<ApiResult> EditUser(EditUserCommand command)
    {
        return CommandResult(await _facade.EditUser(command));
    }

    [HttpPost("registerUser")]
    public async Task<ApiResult> RegisterUser(RegisterUserCommand command)
    {
        return CommandResult(await _facade.RegisterUser(command));
    }

    [HttpGet("{userId}")]
    public async Task<ApiResult<UserDTO?>> GetUserById(long userId)
    {
        return QueryResult(await _facade.GetUserById(userId));
    }

    [HttpGet("{phoneNumber}")]
    public async Task<ApiResult<UserDTO?>> GetUserByPhoneNumber(string phoneNumber)
    {
        return QueryResult(await _facade.GetUserByPhoneNumber(phoneNumber));
    }

    [HttpGet]
    public async Task<ApiResult<UserFilterResult>> GetUsersByFilter([FromForm] UserFilterParams filterParams)
    {
        return QueryResult(await _facade.GetUserByFilter(filterParams));
    }
}