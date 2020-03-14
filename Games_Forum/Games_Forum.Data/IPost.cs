using Games_Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games_Forum.Data
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLatestPosts(int n);


        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
        
    }
}
