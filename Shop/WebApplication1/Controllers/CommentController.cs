using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Domain.RoleAgg;
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

    [PermissionChecker(Permission.Comment_Management)]
    [HttpGet]
    public async Task<ApiResult<CommentFilterResult>> GetCommentsByFilter([FromQuery] CommentFilterParams filterParams)
    {
        var result = await _facade.GetCommentsByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("productComments")]
    public async Task<ApiResult<CommentFilterResult>> GetProductComments(int pageId = 1, int take = 10, int productId = 0)
    {
        var result = await _facade.GetCommentsByFilter(new CommentFilterParams
        {
            ProductId = productId,
            Take = take,
            PageId = pageId,
            CommentStatus = Domain.CommentAgg.CommentStatus.Accepted
        });
        return QueryResult(result);
    }

    [PermissionChecker(Permission.Comment_Management)]
    [HttpGet("{commentId}")]
    public async Task<ApiResult<CommentDto?>> GetCommentsById(long commentId)
    {
        var result = await _facade.GetCommentById(commentId);
        return QueryResult(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<ApiResult> CreateComment(CreateCommentCommand command)
    {
        var result = await _facade.CreateComment(command);
        return CommandResult(result);
    }

    [Authorize]
    [HttpPost("edit")]
    public async Task<ApiResult> EditComment(EditCommentCommand command)
    {
        var result = await _facade.EditComment(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Comment_Management)]
    [HttpPut("changeStatus")]
    public async Task<ApiResult> ChangeCommentStatus(ChangeCommentStatusCommand command)
    {
        var result = await _facade.ChangeStatus(command);
        return CommandResult(result);
    }
}