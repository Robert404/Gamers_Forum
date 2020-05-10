using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace Games_Forum.Tests
{
    [TestFixture]
    public class PostServiceTests
    {
        [TestCase("coffee", 3)]
        [TestCase("posT", 1)]
        [TestCase("tEa", 1)]
        [TestCase("Corona",0)]
        public void Return_Filtered_Results_Similar_To_Query(string query, int expected) 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            //Arrange
            using (var ctx = new ApplicationDbContext(options)) 
            {
                ctx.Forums.Add(new Forum
                {
                    Id = 19
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = 23523,
                    Title = "First Post",
                    Content = "Coffee"
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = -23523,
                    Title = "Coffee",
                    Content = "Content"
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = 232,
                    Title = "Tea",
                    Content = "Coffee"
                });

                ctx.SaveChanges();

                
            }

            //Act
            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetFilteredPosts(query);
                var postCount = result.Count();

                //Assert
                Assert.AreEqual(expected, postCount);
            }
        }
    }
}
