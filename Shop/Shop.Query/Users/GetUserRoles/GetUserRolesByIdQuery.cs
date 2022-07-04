using Shop.Domain.RoleAgg.Repository;
using Shop.Domain.UserAgg.Repository;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Users.GetUserRoles;
public record GetUserRolesByIdQuery(long UserId) : IQuery<List<RoleDTO>>;

public class GetUserRolesByIdQueryHandler : IQueryHandler<GetUserRolesByIdQuery, List<RoleDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    public GetUserRolesByIdQueryHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<List<RoleDTO>> Handle(GetUserRolesByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId);
        if (user == null) return null;

        var userRoleId = user.UserRoles.Where(a => a.UserId == user.Id).Select(a => a.RoleId).FirstOrDefault();

        var userRoles = await _roleRepository.GetAsync(userRoleId);
        if (userRoles == null) return null;

        var permissions = userRoles.Permissions.Select(a => a.Permission).ToList();

        var model = new List<RoleDTO>
        {
            new RoleDTO
            {
                CreationDate = userRoles.CreationDate,
                Id = userRoles.Id,
                Permissions = permissions,
                Title = userRoles.Title
            }
        };
        return model;
    }
}