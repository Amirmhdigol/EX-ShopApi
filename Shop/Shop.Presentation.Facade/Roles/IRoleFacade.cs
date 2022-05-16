using Common.Application;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Query.Roles.DTOs;

namespace Shop.Presentation.Facade.Roles;
public interface IRoleFacade
{
    //Commands
    Task<OperationResult> Create(CreateRoleCommand command);
    Task<OperationResult> Edit(EditRoleCommand command);

    //Queries
    Task<RoleDTO> GetRoleById(long roleId);
    Task<List<RoleDTO>> GetRoles();
}
