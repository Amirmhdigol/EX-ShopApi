using Common.Application;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comments.Delete;

public class DeleteCommentCommandHandler : IBaseCommandHandler<DeleteCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    public DeleteCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetTracking(request.CommentId);
        if (comment == null) return OperationResult.NotFound();

        _commentRepository.Delete(comment);
        await _commentRepository.Save();
        return OperationResult.Success();
    }
}
