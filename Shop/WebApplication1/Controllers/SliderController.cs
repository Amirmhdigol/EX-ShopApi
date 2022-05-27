using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Presentation.Facade.SiteEntities.Sliders;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Domain.RoleAgg.Permission.CRUD_Slider)]
public class SliderController : ApiController
{
    private readonly ISliderFacade _facade;
    public SliderController(ISliderFacade facade)
    {
        _facade = facade;
    }

    [HttpPost]
    public async Task<ApiResult> CreateSlider([FromForm] CreateSliderCommand command)
    {
        return CommandResult(await _facade.Create(command));
    }

    [HttpPut]
    public async Task<ApiResult> EditSlider([FromForm] EditSliderCommand command)
    {
        return CommandResult(await _facade.Edit(command));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ApiResult<List<SliderDTO>>> GetSlidersList()
    {
        return QueryResult(await _facade.GetSlidersList());
    }

    [HttpGet("{sliderId}")]
    public async Task<ApiResult<SliderDTO>> GetSliderById(long sliderId)
    {
        return QueryResult(await _facade.GetSliderById(sliderId));
    }

}
