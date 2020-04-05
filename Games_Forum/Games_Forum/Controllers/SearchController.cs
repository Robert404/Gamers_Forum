using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Models.Forum;
using Games_Forum.Models.Post;
using Games_Forum.Models.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_Forum.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPost _postService;
        ApplicationUser appUser = new ApplicationUser();

        public SearchController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Index(string searchQuery)
        {
            var posts = _postService.GetFilteredPosts(searchQuery).ToList();
            var areNoResult = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());
           

            var postListing = posts.Select(p => new PostListingModel
            {
                AuthorId = p.User.Id,
                AuthorName = p.User.UserName,
                Id = p.Id,
                AuthorRating = appUser.Rating,
                DatePosted = p.Created.ToString(),
                RepliesCount = p.Replies.Count(),
                Title = p.Title,
                Forum = BuildForumListing(p)
            });

            var model = new SearchResultModel
            {
                Posts = postListing,
                SearchQuery = searchQuery,
                EmptySearchResult = areNoResult
                
            };


            return View(model);
        }

        private ForumListingModel BuildForumListing(Post p)
        {
            var forum = p.Forum;

            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }

        [HttpPost]
        public IActionResult Search(string searchQuery) 
        {
            return RedirectToAction("Index", new { searchQuery });
        }
    }
}
