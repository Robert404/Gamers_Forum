﻿using Games_Forum.Data;
using Games_Forum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_Forum.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = GetById(id);
             _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Post post)
        {
            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Replies).ThenInclude(reply => reply.User)
                .Include(p => p.Forum);
        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies)
                    .ThenInclude(post => post.User)
                .Include(post => post.Forum).FirstOrDefault();
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum ,string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery) ? forum.Posts :
             forum.Posts.Where(post => post.Title.Contains(searchQuery) || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            if (searchQuery == null) 
            {
                return GetAll();
            }
            return GetAll().Where(post => post.Title.ToLower().Contains(searchQuery.ToLower()) || post.Content.ToLower().Contains(searchQuery.ToLower()));
        }

        public IEnumerable<Post> GetLatestPosts(int n)
        {
            return GetAll().OrderByDescending(p => p.Created).Take(n);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _context.Forums.Where(f => f.Id == id).First().Posts;
        }
    }
}
