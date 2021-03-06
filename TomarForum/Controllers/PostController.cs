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

        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexViewModel
            {
                Id=post.Id,
                Title=post.Title,
                AuthorId=post.User.Id,
                AuthorName=post.User.UserName,
                AuthorImageUrl=post.User.ProfileImageUrl,
                AuthorRating=post.User.Rating,
                DateCreated=post.DateCreated,
                PostContent=post.Content,//Try to correct this naming!!! One of them is PostContent and one of the is only content.
                Replies =replies,
                ForumId=post.Forum.Id,
                ForumTitle=post.Forum.Title,
                IsAuthorAdmin=CheckAuthorAuthorization(post.User)
            };
            return View(model);
        }

        public IActionResult Create(int id)
        {
            var forum = _forumService.GetById(id);

            var model = new NewPostViewModel
            {
                ForumTitle=forum.Title,
                ForumId=forum.Id,
                ForumImageUrl=forum.ImageUrl,
                AuthorName=User.Identity.Name
            };

            return View(model);
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
            var post = _postService.GetById(id);

            var model = new PostEditViewModel
            {
                Title = post.Title,
                Content = post.Content
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var post = await _postBLL.GetPostEditViewModel(id, User);

            if (post.Result is null)
            {
                return View(post.Value);
            }
            return post.Result;
        }
        private bool CheckAuthorAuthorization(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user).Result.Contains("Admin");
        }

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

        private IEnumerable<PostReplyViewModel> BuildPostReplies(IEnumerable<TomarForumData.EntityModels.PostReply> replies)
        {
            return replies.Select(reply => new PostReplyViewModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                DateCreated = reply.DateCreated,
                ReplyContent = reply.Content,//Try to correct this naming!!! One of them is ReplyContent and one of the is only content.
                IsAuthorAdmin=CheckAuthorAuthorization(reply.User)
            });
        }
    }
}
