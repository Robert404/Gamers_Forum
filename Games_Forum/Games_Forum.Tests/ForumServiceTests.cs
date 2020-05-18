using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Models.Forum;
using Games_Forum.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games_Forum.Tests
{
    public class ForumServiceTests
    {
        [Test]
        public async Task Create_Forum_Creates_New_Forum_Via_Context()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_Forum_Test").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                var forumService = new ForumService(ctx);

                var forum = new Forum
                {
                    Title = "Test Forum Title",
                    Description = "New Forum"
                };

                await forumService.Create(forum);
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                Assert.AreEqual(1, ctx.Forums.CountAsync().Result);
                Assert.AreEqual("Test Forum Title", ctx.Forums.SingleAsync().Result.Title);
            }
        }

        [Test]
        public void Get_All_Forums_Equal_To_GetAll()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_Forums").Options;

            //Arrange
            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Forums.Add(new Forum
                {
                    Id = 228,
                    Title = "first forum"
                });

                ctx.Forums.Add(new Forum
                {
                    Id = 222,
                    Title = "second forum"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options))
            {
                var forumService = new ForumService(ctx);
                var forums = forumService.GetAll();

                //Assert
                Assert.AreEqual(forums.Count(), 2);
            }
        }

        [Test]
        public void Return_Forum_Correctly_By_ForumId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_Forums").Options;

            //Arrange
            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Forums.Add(new Forum
                {
                    Id = 2500,
                    Title = "Nice test"
                });

                ctx.Forums.Add(new Forum
                {
                    Id = 10,
                    Title = "Awesome test"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options))
            {
                var forumService = new ForumService(ctx);
                var forum = forumService.GetById(10);

                //Assert
                Assert.AreEqual(forum.Title, "Awesome test");
            }
        }

        [Test]
        public async Task Edit_Forum_Correctrly_By_EditForum() 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_Forum").Options;

            //Arrange
            using (var ctx = new ApplicationDbContext(options)) 
            {
                ctx.Forums.Add(new Forum
                {
                    Id = 228,
                    Title = "Test Forum",
                    Description = "Desc for test forum"
                });

                ctx.Forums.Add(new Forum
                {
                    Id = 554,
                    Title = "Second Forum",
                    Description = "desc for second forum"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options)) 
            {
                var forumService = new ForumService(ctx);
                var newForum = new EditForumModel { Title = "Edited Title", Description = "Edited desc" };
                var forum = ctx.Forums.Find(228);
                forum.Title = newForum.Title;
                forum.Description = newForum.Description;
                await forumService.Edit(forum);

                //Assert
                Assert.AreEqual(newForum.Title, forum.Title);
                Assert.AreEqual(newForum.Description, forum.Description);
            }
        }
    }
}
