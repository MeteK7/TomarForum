using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel.ApplicationUserViewModel;

namespace TomarForumUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IUploadService _uploadService;
        private readonly IProfileBLL _profileBLL;
        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUserService applicationUserService, IUploadService uploadService, IProfileBLL profileBLL)
        {
            _userManager = userManager;
            _applicationUserService = applicationUserService;
            _uploadService = uploadService;
            _profileBLL = profileBLL;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string id)
        {
            var user = _applicationUserService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var profile = _profileBLL.GetProfile(id);

            return View(profile);
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadProfileImage(IFormFile formFile)
        //{
        //    var userId = _userManager.GetUserId(User);

        //    return View();
        //}
    }
}
