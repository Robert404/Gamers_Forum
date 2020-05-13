using Games_Forum.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games_Forum.Data
{
    public interface IForum
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();

        Task Create(Forum forum);
        Task Delete(int forumId);
        Task Edit(Forum forum);
        IEnumerable<IdentityUser> GetAllActiveUsers(int id);
        bool HasRecentPost(int id);
    }    
}
