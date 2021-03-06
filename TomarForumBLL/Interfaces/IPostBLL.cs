using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TomarForumViewModel.PostViewModels;

namespace TomarForumBLL.Interfaces
{
    public interface IPostBLL
    {
        Task<ActionResult<PostEditViewModel>> GetPostEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
    }
}
