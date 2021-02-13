using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumData;
using TomarForumService.Interfaces;

namespace TomarForumUI.Controllers
{
    public class PostController : Controller
    {
        public readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);

            var model=new PostIndexViewModel
            {

            }
            return View();
        }
    }
}
