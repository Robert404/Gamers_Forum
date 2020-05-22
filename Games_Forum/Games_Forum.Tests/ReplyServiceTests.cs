using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Models.Reply;
using Games_Forum.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Games_Forum.Tests
{
    class ReplyServiceTests
    {
        [Test]
        public async Task Edit_PostReply_Correctly_By_EditPostReply()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_Post").Options;

            //Arrange
            using (var ctx = new ApplicationDbContext(options)) 
            {
                ctx.PostReplies.Add(new PostReply
                {
                    Id = 221,
                    Content = "First Post Reply"
                });

                ctx.PostReplies.Add(new PostReply
                {
                    Id = 123,
                    Content = "Second Post Reply"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options)) 
            {
                var replyService = new ReplyService(ctx);
                var newReply = new ReplyEditModel { ReplyContent = "Updated Content" };
                var reply = ctx.PostReplies.Find(221);
                reply.Content = newReply.ReplyContent;
                await replyService.Edit(reply);

                //Assert
                Assert.AreEqual(newReply.ReplyContent, reply.Content);
            }
        }


        [Test]
        public void Return_PostReply_Correctly_By_PostReplyId() 
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Post_By_Id").Options;

            using (var ctx = new ApplicationDbContext(options)) 
            {
                ctx.PostReplies.Add(new PostReply
                {
                    Id = 221,
                    Content = "Test Reply"
                });

                ctx.PostReplies.Add(new PostReply
                {
                    Id = 111,
                    Content = "Second Reply"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options)) 
            {
                var replyService = new ReplyService(ctx);
                var reply = replyService.GetById(221);

                //Assert
                Assert.AreEqual(reply.Content, "Test Reply");
            }
        }
    }
}
