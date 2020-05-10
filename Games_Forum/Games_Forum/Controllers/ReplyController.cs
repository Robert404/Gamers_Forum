﻿using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_Forum.Controllers
{
    public class ReplyController:Controller
    {
        private readonly IPost _postService;
        private readonly IReply _replyService;
        private readonly UserManager<IdentityUser> _userManager;
        ApplicationUser appUser = new ApplicationUser();

        public ReplyController(IPost postService, UserManager<IdentityUser> userManager, IReply replyService) 
        {
            _postService = postService;
            _userManager = userManager;
            _replyService = replyService;
        }
        public async Task<IActionResult> Create(int id) 
        {
            var post = _postService.GetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,
                AuthorId = user.Id,
                AuthorName = user.UserName,
                IsAuthorAdmin = User.IsInRole("Admin"),
                Created = DateTime.Now,
                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title,
                ForumImageUrl = post.Forum.ImageUrl, 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyModel model) 
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);


            await _postService.AddReply(reply);
            return RedirectToAction("Index", "Post", new { id = model.PostId});
        }

        public async Task<IActionResult> DeleteReply(int id) 
        {
            await _replyService.Delete(id);
            return RedirectToAction("Index","Home");
        }

        private PostReply BuildReply(PostReplyModel model, IdentityUser user)
        {
            var post = _postService.GetById(model.PostId);
            return new PostReply
            {
                Post = post,
                Content = model.ReplyContent,
                Created = DateTime.Now,
                User = user
            };
        }
    }
}
