using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games_Forum.Data
{
    public class SeedData
    {
        ApplicationDbContext _context;

        public SeedData(ApplicationDbContext context) 
        {
            _context = context;
        }

        public Task SeedAdminUser() 
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var userStore = new UserStore<IdentityUser>(_context);
            

            var user = new IdentityUser
            {
                UserName = "ForumAdmin",
                NormalizedUserName = "forumadmin",
                Email = "rob.ners@mail.ru",
                NormalizedEmail = "rob.ners@mail.ru",
                EmailConfirmed = true,
                LockoutEnabled = false, 
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var hasher = new PasswordHasher<IdentityUser>();
            var hashedPassword = hasher.HashPassword(user, "admin");
            user.PasswordHash = hashedPassword;

            var hasAdminRole = _context.Roles.Any(r => r.Name == "Admin");
            if (!hasAdminRole) 
            {
                roleStore.CreateAsync(new IdentityRole 
                {
                    Name = "Admin", 
                    NormalizedName = "admin"
                });
            }

            var hasAdminUser = _context.Users.Any(u => u.NormalizedUserName == user.UserName);

            if (!hasAdminUser) 
            {
                 userStore.CreateAsync(user);
                 userStore.AddToRoleAsync(user, "Admin");
            }
             _context.SaveChangesAsync();

            return Task.CompletedTask;
        }
    }
}
