using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Games_Forum.Tests
{
    [TestFixture]
    public class PostServiceTests
    {
        [TestCase("coffee", 3)]
        [TestCase("posT", 1)]
        [TestCase("tEa", 1)]
        [TestCase("Corona", 0)]
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


        [Test]
        public void Return_Post_Correctly_By_PostId()
        {

            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Post_By_Id").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post
                {
                    Id = 1986,
                    Title = "Test Post",
                });

                ctx.Posts.Add(new Post
                {
                    Id = 1522,
                    Title = "Another Test",
                });
                ctx.SaveChanges();
            }


            //Act
            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var post = postService.GetById(1986);

                //Assert
                Assert.AreEqual(post.Title, "Test Post");
            }

        }

        [Test]
        public void Get_All_Posts_Equal_To_GetAll() 
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Post_By_Id").Options;

            using (var ctx = new ApplicationDbContext(options)) 
            {
                ctx.Add(new Post
                {
                    Id = 2551,
                    Title = "first Title"
                });

                ctx.Add(new Post
                {
                    Id = 600,
                    Title = "secont Title"
                });

                ctx.Add(new Post
                {
                    Id = 660,
                    Title = "third Title"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options)) 
            {
                var postService = new PostService(ctx);
                var posts = postService.GetAll();

                //Assert
                Assert.AreEqual(posts.Count(), 3);
            }
        }

        [Test]
        public async Task Create_Post_Creates_New_Post_Via_Context() 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Post_Writes_Post_To_Database").Options;

            //Arrange
            using (var ctx = new ApplicationDbContext(options)) 
            {
                var postService = new PostService(ctx);
                var post = new Post
                {
                    Title = "New Test Post",
                    Content = "some content in post"
                };

                await postService.Add(post);
            }

            //Act
            using (var ctx = new ApplicationDbContext(options)) 
            {
                //Assert
                Assert.AreEqual(1, ctx.Posts.CountAsync().Result);
                Assert.AreEqual("New Test Post" , ctx.Posts.SingleAsync().Result.Title);
            }
        }
    }
}

