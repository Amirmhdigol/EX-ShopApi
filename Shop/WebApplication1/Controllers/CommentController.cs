using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments.DTOs;

namespace Shop.Api.Controllers;

public class CommentController : ApiController
{
    private readonly ICommentFacade _facade;
    public CommentController(ICommentFacade facade)
    {
        _facade = facade;
    }

    [HttpGet]
    public async Task<ApiResult<CommentFilterResult>> GetCommentsByFilter([FromQuery] CommentFilterParams filterParams)
    {
        var result = await _facade.GetCommentsByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{commentId}")]
    public async Task<ApiResult<CommentDto?>> GetCommentsByFilter(long commentId)
    {
        var result = await _facade.GetCommentById(commentId);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> CreateComment(CreateCommentCommand command)
    {
        var result = await _facade.CreateComment(command);
        return CommandResult(result);
    }

    [HttpPut("changeStatus")]
    public async Task<ApiResult> ChangeCommentStatus(ChangeCommentStatusCommand command)
    {
        var result = await _facade.ChangeStatus(command);
        return CommandResult(result);
    }
}