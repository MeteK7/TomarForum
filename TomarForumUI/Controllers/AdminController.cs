using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;

namespace TomarForumUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminBLL _adminBLL;
        private readonly IPostBLL _postBLL;
        private readonly IProfileBLL _profileBLL;
        public AdminController(IAdminBLL adminBLL, IPostBLL postBLL, IProfileBLL profileBLL)
        {
            _adminBLL = adminBLL;
            _postBLL = postBLL;
            _profileBLL = profileBLL;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllPostsOfUser()
        {
            return View(await _postBLL.GetAdminPosts(User));
        }

        public async Task<IActionResult> EditUserProfile()
        {
            return View(await _profileBLL.GetProfileEditViewModel(User));
        }
    }
}