using AutoMapper;
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.ChangePassword;

namespace Shop.Api.Infrastructure;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<AddUserAddressCommand, AddUserAddressViewModel>().ReverseMap()
            .ForMember(f => f.PhoneNumber, r => r.MapFrom(w => w.PhoneNumber)).ReverseMap(); ;
       
        CreateMap<ChangePasswordViewModel, ChangePasswordCommand>().ReverseMap();
    }
}