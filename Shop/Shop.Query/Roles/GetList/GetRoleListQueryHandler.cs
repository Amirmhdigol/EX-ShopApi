using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Roles.DTOs;
namespace Shop.Query.Roles.GetList;

public class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, List<RoleDTO>>
{
    private readonly ShopContext _context;
    public GetRoleListQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<List<RoleDTO>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(r => new RoleDTO
        {
            Id = r.Id,
            CreationDate = r.CreationDate,
            Permissions = r.Permissions.Select(a => a.Permission).ToList(),
            Title = r.Title,
        }).ToListAsync(cancellationToken);
    }
}