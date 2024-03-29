﻿using AutoMapper;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.SetAddressActive;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.Users.Addresses.DTOs;

namespace Shop.Api.Controllers;

[Authorize]
public class UserAddressController : ApiController
{
    private readonly IUserAddressFacade _facade;
    private readonly IMapper _mapper;
    public UserAddressController(IUserAddressFacade facade, IMapper mapper)
    {
        _facade = facade;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ApiResult> AddAddress(AddUserAddressViewModel viewModel)
    {
        var command = new AddUserAddressCommand(User.GetUserId(), viewModel.Province, viewModel.City, viewModel.Name,
            viewModel.Family, viewModel.PostalAddress, viewModel.PostalCode, viewModel.NationalCode,
            new PhoneNumber(viewModel.PhoneNumber));

        command.UserId = User.GetUserId();
        return CommandResult(await _facade.AddAddress(command));
    }

    [HttpPut]
    public async Task<ApiResult> EditAddress(EditUserAddressViewModel viewModel)
    {
        var command = new EditUserAddressCommand(viewModel.Province, viewModel.City, viewModel.Name,
            viewModel.Family, viewModel.PostalAddress, viewModel.PostalCode, viewModel.NationalCode,
            new PhoneNumber(viewModel.PhoneNumber), viewModel.Id);

        command.UserId = User.GetUserId();
        return CommandResult(await _facade.EditAddress(command));
    }
    [HttpPut("SetActiveAddress/{addressId}")]
    public async Task<ApiResult> SetAddressActive(long addressId)
    {
        var command = new SetUserAddressActiveCommand(User.GetUserId(), addressId);
        return CommandResult(await _facade.SetAddressActive(command));
    }
    [HttpDelete("{addressId}")]
    public async Task<ApiResult> Delete(long addressId)
    {
        var result = await _facade.DeleteAddress(new DeleteUserAddressCommand(User.GetUserId(), addressId));
        return CommandResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<List<AddressDTO>>> GetList()
    {
        var result = await _facade.GetAddressesList(User.GetUserId());
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<AddressDTO?>> GetById(long id)
    {
        var result = await _facade.GetAddressById(id);
        return QueryResult(result);
    }
}