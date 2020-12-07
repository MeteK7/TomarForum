using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarBlogUI.BusinessManagers.Interfaces;
using TomarBlogUI.Models.PostViewModels;

namespace TomarBlogUI.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostBusinessManager postBusinessManager;
        public PostController(IPostBusinessManager postBusinessManager)
        {
            this.postBusinessManager = postBusinessManager;
        }

        [Route("Post/{id}"), AllowAnonymous]
        public async Task<IActionResult> Index(int? id)
        {
            var actionResult = await postBusinessManager.GetPostViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var actionResult = await postBusinessManager.GetEditViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createViewModel) //You can name this action result as "Create" because it has post attribute so that there won't be a conflict.
        {
            await postBusinessManager.CreatePost(createViewModel, User);
            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var actionResult= await postBusinessManager.UpdatePost(editViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Edit", new { editViewModel.Post.Id });

            return actionResult.Result;
        }
    }
}
