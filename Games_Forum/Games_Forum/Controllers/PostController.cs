using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Models.Post;
using Games_Forum.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private readonly UserManager<IdentityUser> _userManager;
        ApplicationUser appUser = new ApplicationUser();
        public PostController(IPost postService, IForum forumService, UserManager<IdentityUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
        }

        public IActionResult Index(int id) 
        {
            var post = _postService.GetById(id);
            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = appUser.ProfileImageUrl,
                AuthorRating = appUser.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Replies = replies,
                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title,
                IsAuthorAdmin = IsAuthorAdmin(post.User)
            };


            return View(model);
        }


        public IActionResult Create(int id) 
        {
            var forum = _forumService.GetById(id);

            var model = new NewPostModel
            {
                ForumId = forum.Id,
                ForumName = forum.Title,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };
            return View(model);
        }

        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.Delete(id);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user =  _userManager.FindByIdAsync(userId).Result;

            var post = BuildPost(model, user);

            _postService.Add(post).Wait();
            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private Post BuildPost(NewPostModel model, IdentityUser user)
        {
            var forum = _forumService.GetById(model.ForumId);
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Forum = forum
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(r => new PostReplyModel
            {
                Id = r.Id,
                AuthorId = r.User.Id,
                AuthorName = r.User.UserName,
                Created = r.Created,
                PostId = r.Post.Id,
                ReplyContent = r.Content,
                IsAuthorAdmin = IsAuthorAdmin(r.User)
            });
        }

        private bool IsAuthorAdmin(IdentityUser user)
        {
            return _userManager.GetRolesAsync(user).Result.Contains("Admin");
        }
    }
}
