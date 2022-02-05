using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelGuide.Entities.Messages
{
    public enum ErrorMessageCode
    {
        MemberNotFound = 101,
        EmailAlreadyExists = 102,
        UsernameAlreadyExists = 103,
        MemberCouldNotInserted = 104,
        MemberCouldNotUpdated = 105,
        MemberCouldNotDelete = 106
    }
}
