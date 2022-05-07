using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Create
{
    public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
    {
        private readonly IRoleRepository _repository;

        public CreateRoleCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var Permissions = new List<RolePermission>();
            request.Permissions.ForEach(f =>
            {
                Permissions.Add(new RolePermission(f));
            });
          
            var role = new Role(request.title, Permissions);
            
             _repository.Add(role);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
