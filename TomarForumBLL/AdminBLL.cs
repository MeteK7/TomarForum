using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumViewModel.AdminViewModels;

namespace TomarForumBLL
{
    public class AdminBLL:IAdminBLL
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminBLL(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AdminIndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);
            return new AdminIndexViewModel
            {
                Posts = postService.GetPosts(applicationUser)
            };
        }
    }
}
