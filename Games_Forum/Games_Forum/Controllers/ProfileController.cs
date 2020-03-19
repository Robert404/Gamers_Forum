using Games_Forum.Data;
using Games_Forum.Data.Models;
using Games_Forum.Models.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_Forum.Controllers
{
    public class ProfileController:Controller
    {
        ApplicationUser appUser = new ApplicationUser();
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        public ProfileController(UserManager<IdentityUser> userManager, IApplicationUser userService, IUpload uploadService) 
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
        }

        public IActionResult Detail(string id) 
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var model = new ProfileModel
            {
                UserName = user.UserName,
                Email = user.Email,
                UserId = user.Id,
                UserRating = appUser.Rating,
                MemberSince = user.MemberSince,
                IsAdmin = userRoles.Contains("Admin"),
                ProfileImageUrl = user.ProfileImageUrl
            };

            return View(model);
        }
    }
}
