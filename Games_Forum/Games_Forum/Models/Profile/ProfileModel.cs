using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_Forum.Models.Profile
{
    public class ProfileModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int UserRating { get; set; }
        public string ProfileImageUrl { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime MemberSince { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
