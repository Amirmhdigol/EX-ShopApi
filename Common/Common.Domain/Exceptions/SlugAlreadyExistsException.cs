using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Exceptions
{
    public class SlugAlreadyExistsException : BaseDomainException
    {
        public SlugAlreadyExistsException()
        {

        }
        public SlugAlreadyExistsException(string message) : base(message)
        {
            
        }
    }
}
