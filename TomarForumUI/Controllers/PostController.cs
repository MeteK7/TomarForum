using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData;
using TomarForumData.EntityModels;
using TomarForumService;
using TomarForumService.Interfaces;
using TomarForumViewModel.PostViewModels;
using TomarForumViewModel.ReplyViewModels;

namespace TomarForumUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IForumService _forumService;
        private readonly IPostBLL _postBLL;
        private readonly IForumBLL _forumBLL;
        private static UserManager<ApplicationUser> _userManager;
        public PostController(IPostService postService, IForumService forumService, IPostBLL postBLL, IForumBLL forumBLL, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _postBLL=postBLL;
            _forumBLL = forumBLL;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index(int id)
        {
            var post =  _postBLL.GetPostIndexViewModel(id, User);

            return View(post.Value);
        }

        public IActionResult Create(int id)
        {
            var forum = _forumBLL.GetForumById(id);

            return View(forum);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPostViewModel newPostViewModel)
        {
            #region CREATING A NEW POST
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var post = _postBLL.BuildPost(newPostViewModel, user);

            await _postService.Add(post);
            #endregion

            #region INCREASING THE TOTAL POST AMOUNT IN FORUM
            var forum = _forumService.GetById(newPostViewModel.ForumId);
            forum.AmountTotalPost += 1;
            #endregion

            #region INSERTING NEW FORUM AMOUNT
            bool userFirstPostByForum = _forumService.CheckUserFirstPostByForum(userId.ToString(), newPostViewModel.ForumId);

            //If it is the first time that the user is creating a post related with the forum, then increase one user of the forum bcz a new user has just used the forum for the first time.
            if (userFirstPostByForum==true)
            {
                var model = _forumBLL.GetForumUserNewAmount(user, forum);
                await _forumService.AddNewUser(model);
            }
            #endregion
            
            await _forumService.Update(forum);

            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        public IActionResult Edit(int? id)
        {
            var post = _postBLL.GetPostEditViewModel(id, User);

            return View(post.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostEditViewModel postEditViewModel)
        {
            var result = await _postBLL.UpdatePost(postEditViewModel, User);

            if (result.Result is null)
                return RedirectToAction("Edit", new { postEditViewModel.Post.Id });

            return result.Result;
        }
    }
}
