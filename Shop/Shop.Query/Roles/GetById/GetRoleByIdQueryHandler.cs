using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Roles.DTOs;
namespace Shop.Query.Roles.GetById;

internal class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDTO>
{
    private readonly ShopContext _context;
    public GetRoleByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<RoleDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(a => a.Id == request.RoleId, cancellationToken);
        if (role == null) return null;
        return new RoleDTO()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(a => a.Permission).ToList(),
            Title = role.Title,
        };
    }
}