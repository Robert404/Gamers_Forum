using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games_Forum.Tests
{
    public class ApplicationUserServiceTests
    {
        [Test]
        public void Get_All_Users_Equal_To_GetAll() 
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_Users").Options;

            using (var ctx = new ApplicationDbContext(options)) 
            {
                ctx.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "228",
                    UserName = "Oleg"
                });

                ctx.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "3332",
                    UserName = "Chel"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options)) 
            {
                var appUserService = new ApplicationUserService(ctx);
                var users = appUserService.GetAll();

                //Assert
                Assert.AreEqual(users.Count(), 2);
            }
        }

        [Test]
        public void Return_User_Correctly_By_UserId() 
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Users_By_Id").Options;

            using (var ctx = new ApplicationDbContext(options)) 
            {
                ctx.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "111",
                    UserName = "Leonard"
                });

                ctx.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "3332",
                    UserName = "Chel"
                });

                ctx.SaveChanges();
            }


            //Act
            using (var ctx = new ApplicationDbContext(options)) 
            {
                var appUserService = new ApplicationUserService(ctx);
                var user = appUserService.GetById("111");

                //Assert
                Assert.AreEqual(user.UserName, "Leonard");
            }
        }   
        
    }
}
