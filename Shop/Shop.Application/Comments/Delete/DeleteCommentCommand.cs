using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Comments.Delete;
public record DeleteCommentCommand(long CommentId) : IBaseCommand;
