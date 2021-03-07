using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumViewModel.ApplicationUserViewModel;

namespace TomarForumBLL
{
    public class ProfileBLL:IProfileBLL
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;
        public ProfileBLL(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ProfileViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {

        }
    }
}
