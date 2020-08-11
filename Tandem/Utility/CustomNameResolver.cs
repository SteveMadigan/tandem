using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tandem.Model;
using Tandem.Responses;

namespace Tandem.Utility
{
    public class CustomNameResolver: IValueResolver<User, GetUserResponse, string>
    {
        public string Resolve(User source, GetUserResponse destination, string destMember, ResolutionContext context)
        {
            return source.FirstName + " " + source.MiddleName + " " + source.LastName;
        }
    }
}
