using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TomarForumData.EntityModels;
using TomarForumViewModel.PostViewModels;

namespace TomarForumBLL.Interfaces
{
    public interface IPostBLL
    {
        ActionResult<PostIndexViewModel> GetPostIndexViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        ActionResult<PostEditViewModel> GetPostEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        //ActionResult<PostEditViewModel> UpdatePost(PostEditViewModel postEditViewModel, ClaimsPrincipal claimsPrincipal);
        Post BuildPost(NewPostViewModel newPostViewModel, ApplicationUser user);
        ForumUser InsertForumUserAmount(ApplicationUser user, Forum forum);
    }
}
