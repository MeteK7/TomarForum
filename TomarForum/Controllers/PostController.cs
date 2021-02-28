using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumData;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumUI.ViewModels.PostViewModels;
using TomarForumUI.ViewModels.ReplyViewModels;

namespace TomarForumUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IForumService _forumService;
        private static UserManager<ApplicationUser> _userManager;
        public PostController(IPostService postService, IForumService forumService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
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
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(newPostViewModel, user);

            await _postService.Add(post);

            return RedirectToAction("Index", "Post", new { id = post.Id });
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
