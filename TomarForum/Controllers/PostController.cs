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
        private readonly IForumUserService _forumUserService;
        private readonly IPostBLL _postBLL;
        private static UserManager<ApplicationUser> _userManager;
        public PostController(IPostService postService, IForumService forumService, IForumUserService forumUserService, IPostBLL postBLL, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _forumUserService = forumUserService;
            _postBLL=postBLL;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index(int id)
        {
            var post =  _postBLL.GetPostIndexViewModel(id, User);

            return View(post.Value);
        }

        //public IActionResult Create(int id)
        //{
        //    var forum = _forumService.GetById(id);

        //    var model = new NewPostViewModel
        //    {
        //        ForumTitle=forum.Title,
        //        ForumId=forum.Id,
        //        ForumImageUrl=forum.ImageUrl,
        //        AuthorName=User.Identity.Name
        //    };

        //    return View(model);
        //}

        public IActionResult Create()
        {
            return View(new NewPostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPostViewModel newPostViewModel)
        {
            #region CREATING A NEW POST
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(newPostViewModel, user);

            await _postService.Add(post);
            #endregion

            #region INCREASING THE TOTAL POST AMOUNT IN FORUM
            var forum = _forumService.GetById(newPostViewModel.ForumId);
            forum.AmountTotalPost += 1;
            
            #endregion

            bool userFirstPostByForum = _forumService.CheckUserFirstPostByForum(userId.ToString(), newPostViewModel.ForumId);

            if (userFirstPostByForum==true)//If it is the first time that the user is creating a post related with the forum, then increase one user of the forum.
            {
                forum.AmountTotalUser += 1;

                #region INSERTING FORUM ENTRANCE BY USER
                var model = new ForumUser
                {
                    User = user,
                    Forum = forum
                };

                await _forumUserService.Add(model);
                #endregion
            }

            await _forumService.Update(forum);

            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        public IActionResult Edit(int id)
        {
            var post = _postBLL.GetPostEditViewModel(id, User);

            return View(post.Value);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(PostEditViewModel postEditViewModel)
        //{
        //    var post = await _postBLL.UpdatePost(postEditViewModel, User);

        //    if (actionResult.Result is null)
        //        return RedirectToAction("Edit", new { postEditViewModel.Post.Id });

        //    return actionResult.Result;
        //}

        private Post BuildPost(NewPostViewModel newPostViewModel, ApplicationUser user)
        {
            var forum = _forumService.GetById(newPostViewModel.ForumId);

            return new Post
            {
                Title = newPostViewModel.Title,
                Content = newPostViewModel.Content,
                DateCreated = DateTime.Now,
                User = user,
                Forum=forum
            };
        }

    }
}
