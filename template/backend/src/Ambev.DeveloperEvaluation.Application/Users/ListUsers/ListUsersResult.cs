using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    public class ListUsersResult
    {
        public IQueryable<GetUserResult>? Users { get; set; }
    }
}
