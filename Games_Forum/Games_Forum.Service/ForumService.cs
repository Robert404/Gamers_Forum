using Games_Forum.Data;
using Games_Forum.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_Forum.Service
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;
        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Forum forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            var forum = GetById(forumId);
            _context.Remove(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Forum forum)
        {
            _context.Update(forum);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(f => f.Posts); 
        }

        public IEnumerable<IdentityUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IdentityUser> GetAllActiveUsers(int id)
        {
            var posts = GetById(id).Posts;
            if (posts != null || !posts.Any())
            {
                var postUsers = posts.Select(p => p.User);
                var replyUsers = posts.SelectMany(p => p.Replies).Select(r => r.User);
                return postUsers.Union(replyUsers).Distinct();
            }

            return new List<IdentityUser>();
        }

        public Forum GetById(int id)
        {
            var forum = _context.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts)
                    .ThenInclude(f => f.User)
                .Include(f => f.Posts)
                    .ThenInclude(f => f.Replies)
                        .ThenInclude(f => f.User)
                .FirstOrDefault();

            return forum;
        }

        public bool HasRecentPost(int id)
        {
            const int hoursAgo = 12;
            var window = DateTime.Now.AddHours(hoursAgo);
            return GetById(id).Posts.Any(p => p.Created > window);
        }

        
    }
}
