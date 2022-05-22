using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Presentation.Facade.Roles;
using Shop.Query.Roles.DTOs;

namespace Shop.Api.Controllers;

public class RoleController : ApiController
{
    private readonly IRoleFacade _facade;
    public RoleController(IRoleFacade facade)
    {
        _facade = facade;
    }

    [HttpGet("{roleId}")]
    public async Task<ApiResult<RoleDTO>> GetRoleById(long roleId)
    {
        return QueryResult(await _facade.GetRoleById(roleId));
    }
    
    [HttpGet]
    public async Task<ApiResult<List<RoleDTO>>> GetRolesList()
    {
        return QueryResult(await _facade.GetRoles());
    }

    [HttpPost]
    public async Task<ApiResult> CreateRole(CreateRoleCommand command)
    {
        return CommandResult(await _facade.Create(command));
    }

    [HttpPut]
    public async Task<ApiResult> EditRole(EditRoleCommand command)
    {
        return CommandResult(await _facade.Edit(command));
    }
}