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
using TomarForumViewModel.ApplicationUserViewModel;

namespace TomarForumBLL
{
    public class ProfileBLL:IProfileBLL
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;
        public ProfileBLL(UserManager<ApplicationUser> userManager, IProfileService profileService)
        {
            _userManager = userManager;
            _profileService = profileService;
        }
        public async Task<ProfileViewModel> GetProfileEditViewModel(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser=await _userManager.GetUserAsync(claimsPrincipal);
            return new ProfileViewModel
            {
                UserId = applicationUser.Id,
                Email = applicationUser.Email,
                UserName = applicationUser.UserName,
                ProfileImageUrl = applicationUser.ProfileImageUrl,
                MembershipCreatedOn = applicationUser.MembershipCreatedOn
            };
        }
    }
}
