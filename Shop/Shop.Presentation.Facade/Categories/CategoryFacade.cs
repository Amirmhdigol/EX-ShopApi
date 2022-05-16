﻿using Common.Application;
using MediatR;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.GetById;
using Shop.Query.Categories.GetByParentId;
using Shop.Query.Categories.GetList;

namespace Shop.Presentation.Facade.Categories;

internal class CategoryFacade : ICategoryFacade
{
    private readonly IMediator _mediator;
    public CategoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> AddChild(AddCategoryChildCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<List<CategoryChildDto>> GetCategoriesByParentId(long parentId)
    {
        return await _mediator.Send(new GetCategoryByParentIdQuery(parentId));
    }

    public async Task<CategoryDto> GetCategoryById(long id)
    {
        return await _mediator.Send(new GetCategoryByIdQuery(id));
    }

    public async Task<List<CategoryDto>> GetCategoryList()
    {
        return await _mediator.Send(new GetCategoryListQuery());
    }
}