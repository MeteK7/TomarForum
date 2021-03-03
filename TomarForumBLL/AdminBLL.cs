using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel.AdminViewModels;

namespace TomarForumBLL
{
    public class AdminBLL:IAdminBLL
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostService _postService;
        public AdminBLL(UserManager<ApplicationUser> userManager, IPostService postService)
        {
            _userManager = userManager;
            _postService = postService;
        }
        public async Task<AdminIndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);
            return new AdminIndexViewModel
            {
                Posts = _postService.GetPostsByUser(applicationUser)
            };
        }
    }
}
