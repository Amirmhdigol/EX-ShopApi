using Common.Application;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Query.Users.Addresses.DTOs;

namespace Shop.Presentation.Facade.Users.Addresses;
public interface IUserAddressFacade
{
    Task<OperationResult> AddAddress(AddUserAddressCommand command);
    Task<OperationResult> EditAddress(EditUserAddressCommand command);
    Task<OperationResult> DeleteAddress(DeleteUserAddressCommand command);
        
    Task<AddressDTO?> GetAddressById(long userAddressId);
    Task<List<AddressDTO>> GetAddressesList(long userId);
}