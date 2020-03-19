using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Games_Forum.Models;
using Games_Forum.Models.Home;
using Games_Forum.Data;
using Games_Forum.Models.Post;
using Games_Forum.Data.Models;
using Games_Forum.Models.Forum;

namespace Games_Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPost _postService;
        ApplicationUser appUser = new ApplicationUser();

        public HomeController(ILogger<HomeController> logger, IPost postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var model = BuildHomeIndexModel();
            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var latestPosts = _postService.GetLatestPosts(1);

            var posts = latestPosts.Select(p => new PostListingModel
            {
                Id = p.Id,
                Title = p.Title,
                AuthorId = p.User.Id,
                AuthorName = p.User.UserName,
                Forum = GetForumListingForPost(p),
                RepliesCount = p.Replies.Count(),
                AuthorRating = appUser.Rating,
                DatePosted = p.Created.ToString()
            });

            return new HomeIndexModel
            {
                LatestPosts = posts,
                SearchQuery = ""
            };
        }

        private ForumListingModel GetForumListingForPost(Post post)
        {
            var forum = post.Forum;

            return new ForumListingModel
            {
                Name = forum.Title,
                ImageUrl = forum.ImageUrl,
                Id = forum.Id
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
