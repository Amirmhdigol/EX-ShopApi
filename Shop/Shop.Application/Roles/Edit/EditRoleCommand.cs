using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Roles.Edit;

public record EditRoleCommand(long Id ,string title, List<Permission> Permissions) : IBaseCommand;

public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
{
    private readonly IRoleRepository _repository;

    public EditRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetTracking(request.Id);

        role.Edit(request.title);

        var Permissions = new List<RolePermission>();
        request.Permissions.ForEach(f =>
        {
            Permissions.Add(new RolePermission(f));
        });
        role.SetPermissions(Permissions);
        await _repository.Save();
        return OperationResult.Success();
    }
}