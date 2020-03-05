using Games_Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games_Forum.Data
{
    public interface IForum
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId,string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
    }    
}
