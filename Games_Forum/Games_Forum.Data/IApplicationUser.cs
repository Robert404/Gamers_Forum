using Games_Forum.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Games_Forum.Data
{
    public interface IApplicationUser
    {
        IdentityUser GetById(string id);
        IEnumerable<IdentityUser> GetAll();
        Task SetProfileImage(string id, Uri uri);
        Task IncrementRating(string id, Type type);
    }
}
