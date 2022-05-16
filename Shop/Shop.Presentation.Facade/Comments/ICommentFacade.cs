using Common.Application;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Query.Comments.DTOs;

namespace Shop.Presentation.Facade.Comments;
public interface ICommentFacade
{
    //Commands
    Task<OperationResult> ChangeStatus(ChangeCommentStatusCommand command);
    Task<OperationResult> CreateComment(CreateCommentCommand command);
    Task<OperationResult> EditComment(EditCommentCommand command);

    //Queries
    Task<CommentDto?> GetCommentById(long commentId);
    Task<CommentFilterResult> GetCommentsByFilter(CommentFilterParams filterParams);
}
