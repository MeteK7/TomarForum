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
    }
}
