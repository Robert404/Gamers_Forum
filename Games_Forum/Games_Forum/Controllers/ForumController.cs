﻿using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Models.Forum;
using Games_Forum.Models.Post;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;
        ApplicationDbContext _context;
        ApplicationUser appUser = new ApplicationUser();
        public ForumController(IForum forumService, IPost postService, ApplicationDbContext context)
        {
            _forumService = forumService;
            _postService = postService;
            _context = context;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(forum => new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl,
                NumberOfPosts = forum.Posts?.Count() ?? 0,
                NumberOfUsers = _forumService.GetAllActiveUsers(forum.Id).Count(),
                HasRecentPost = _forumService.HasRecentPost(forum.Id)
            });

            var model = new ForumIndexModel
            {
                ForumList = forums.OrderBy(f => f.Name)
            };

            return View(model);
        }

        public IActionResult Topic(int id, string searchQuery)
        {
            var forum = _forumService.GetById(id);
            var posts = new List<Post>();

            posts = _postService.GetFilteredPosts(forum, searchQuery).ToList();

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = appUser.Rating,
                AuthorName = post.User.UserName,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }

        public IActionResult Create() 
        {
            var model = new AddForumModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddForum(AddForumModel model) 
        {
            var imageUrl = "";
            if (model.ImageUrl == null)
            {
                var forum = new Forum
                {
                    Title = model.Title,
                    Description = model.Description,
                    Created = DateTime.Now,
                    ImageUrl = imageUrl
                };

                await _forumService.Create(forum);
            }

            else 
            {
                var forum = new Forum
                {
                    Title = model.Title,
                    Description = model.Description,
                    Created = DateTime.Now,
                    ImageUrl = model.ImageUrl
                };

                await _forumService.Create(forum);
            }

            return RedirectToAction("Index","Forum");
        }


        public IActionResult Edit(int id)
        {
            var forum = _forumService.GetById(id);
            EditForumModel model = new EditForumModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditForum(EditForumModel model) 
        {
            var forum = _forumService.GetById(model.Id);
            forum.Title = model.Title; 
            forum.Description = model.Description;
            forum.Id = model.Id;
            if (model.ImageUrl != null) 
            {
                forum.ImageUrl = model.ImageUrl;
            }

            await _forumService.Edit(forum);
            return RedirectToAction("Index", "Forum");
        }


        public async Task<IActionResult> DeleteForum(int id) 
        {
            await _forumService.Delete(id);
            return RedirectToAction("Index","Forum");
        }


        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }

        private ForumListingModel BuildForumListing(Forum forum)
        {

            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }


    }
}
