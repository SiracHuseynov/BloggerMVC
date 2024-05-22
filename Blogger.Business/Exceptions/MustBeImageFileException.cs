using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Business.Exceptions
{
    public class MustBeImageFileException : Exception
    {
        public MustBeImageFileException(string? message) : base(message)
        {
        }
    }
}
