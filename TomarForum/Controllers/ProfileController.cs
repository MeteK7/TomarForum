using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumUI.ViewModels.ApplicationUserViewModel;

namespace TomarForumUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IUploadService _uploadService;
        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUserService applicationUserService, IUploadService uploadService)
        {
            _userManager = userManager;
            _applicationUserService = applicationUserService;
            _uploadService = uploadService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string id)
        {
            var model = new ProfileViewModel()
            {

            };
            return View();
        }
    }
}
