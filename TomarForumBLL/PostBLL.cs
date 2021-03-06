using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TomarForumService.Interfaces;
using TomarForumViewModel.PostViewModels;

namespace TomarForumBLL
{
    public class PostBLL
    {
        private readonly IPostService _postService;
        public PostBLL(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<ActionResult<PostEditViewModel>> GetPostEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
            {
                return new BadRequestResult();
            }

            var post = _postService.GetById(id.Value);

            return new PostEditViewModel
            {
                Title = post.Title,
                Content = post.Content
            };
        }
    }
}
