using Games_Forum.Data;
using Games_Forum.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games_Forum.Service
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context;
        ApplicationUser appUser = new ApplicationUser();

        public ApplicationUserService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IEnumerable<IdentityUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public IdentityUser GetById(string id)
        {
            return GetAll().FirstOrDefault(user => user.Id == id);
        }

        public Task IncrementRating(string id, Type type)
        {
            throw new NotImplementedException();
        }

        public async Task SetProfileImage(string id, Uri uri)
        {
            var user = GetById(id);
            appUser.ProfileImageUrl = uri.AbsoluteUri;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
