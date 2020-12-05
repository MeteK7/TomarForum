using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TomarBlogData.Models;
using TomarBlogUI.Models.BlogViewModels;
using TomarBlogUI.Models.HomeViewModels;

namespace TomarBlogUI.BusinessManagers.Interfaces
{
    public interface IBlogBusinessManager
    {
        IndexViewModel GetIndexViewModel(string searchString, int? page);
        Task<Blog> CreateBlog(CreateViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
    }
}
