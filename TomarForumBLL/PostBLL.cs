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
        private readonly IAuthorizationService _authorizationService;
        public PostBLL(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<ActionResult<PostEditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal, IAuthorizationService authorizationService)
        {
            if (id is null)
            {
                return new BadRequestResult();
            }

            var postId = id.Value;

            var post = _postService.GetById(postId);

            if (post is null)
            {
                return new NotFoundResult();
            }

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);

            return new PostEditViewModel
            {
                Post = post
            };
        }

    }
}
