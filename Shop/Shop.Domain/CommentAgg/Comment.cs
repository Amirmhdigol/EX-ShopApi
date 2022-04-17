using Common.Domain.Bases;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.CommentAgg
{
    public class Comment : BaseAggregate
    {
        public Comment(long userId, long productId, string text)
        {
            NullOrEmptyDomainDataException.CheckString(text,nameof(text));
            UserId = userId;
            ProductId = productId;
            Text = text;
            Status = CommentStatus.Pending;
        }

        public long UserId { get; private set; }    
        public long ProductId { get; private set; }
        public string Text { get; private set; }   
        public CommentStatus Status { get; private set; }
        public DateTime UpdateDate { get; private set; }

        public void Edit(string text)
        {
            NullOrEmptyDomainDataException.CheckString(text, nameof(text));
            Text = text;
        }
        public void ChangeStatus(CommentStatus commentStatus)
        {
            Status = commentStatus;
            UpdateDate = DateTime.Now;
        }
    }
}
    