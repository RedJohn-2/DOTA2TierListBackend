using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Exceptions
{
    public class UserNotFoundException : NotFoundException
    { public UserNotFoundException(string msg = "User not found!") : base(msg) { } }

    public class AuthenticationException : Exception
    { public AuthenticationException(string msg = "") : base(msg) { } }

    public class UserDuplicateException : DuplicateException
    { public UserDuplicateException(string msg = "User with this email has already been registered!") : base(msg) { } }

}
