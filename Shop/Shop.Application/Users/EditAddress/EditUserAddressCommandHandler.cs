using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.EditAddress;

internal class EditUserAddressCommandHandler : IBaseCommandHandler<EditUserAddressCommand>
{
    private readonly IUserRepository _repository;

    public EditUserAddressCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var address = new UserAddress(request.Province,request.City,request.Name,request.Family
            ,request.PostalAddress,request.PostalCode,request.NationalCode,request.PhoneNumber);

        user.EditAddress(address,request.Id);
        await _repository.Save();
        return OperationResult.Success();
    }
}

