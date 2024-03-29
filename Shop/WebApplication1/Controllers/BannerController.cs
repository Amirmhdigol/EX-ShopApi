﻿using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Domain.RoleAgg;
using Shop.Presentation.Facade.SiteEntities.Banners;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.CRUD_Banner)]
public class BannerController : ApiController
{
    private readonly IBannerFacade _facade;
  
    public BannerController(IBannerFacade facade)
    {
        _facade = facade;
    }

    [HttpPost]
    public async Task<ApiResult> CreateBanner([FromForm] CreateBannerCommand command)
    {
        return CommandResult(await _facade.Create(command));
    }

    [HttpPut]
    public async Task<ApiResult> EditBanner([FromForm] EditBannerCommand command)
    {
        return CommandResult(await _facade.Edit(command));
    }

    [HttpDelete("{bannerId}")]
    public async Task<ApiResult> DeleteBanner(long bannerId)
    {
        return CommandResult(await _facade.Delete(bannerId));
    }

    [HttpGet("{bannerId}")]
    public async Task<ApiResult<BannerDTO>> GetBannerById(long bannerId)
    {
        return QueryResult(await _facade.GetBannerById(bannerId));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ApiResult<List<BannerDTO>>> GetBannersList()
    {
        return QueryResult(await _facade.GetBannersList());
    }
}
    