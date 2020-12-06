using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TomarBlogData.Models;
using TomarBlogService.Interfaces;
using TomarBlogUI.BusinessManagers.Interfaces;
using TomarBlogUI.Models.AdminViewModels;

namespace TomarBlogUI.BusinessManagers
{
    public class AdminBusinessManager:IAdminBusinessManager
    {
        private UserManager<ApplicationUser> userManager;
        private IPostService postService;

        public AdminBusinessManager(UserManager<ApplicationUser> userManager, IPostService postService)
        {
            this.userManager = userManager;
            this.postService = postService;
        }
        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel {
                Posts = postService.GetPosts(applicationUser)
            };
        }
    }
}
