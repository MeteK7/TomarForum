using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumData;
using TomarForumService.Interfaces;
using TomarForumUI.ViewModels.PostViewModels;
using TomarForumUI.ViewModels.ReplyViewModels;

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
                Replies =replies

            };
            return View(model);
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
                ReplyContent = reply.Content//Try to correct this naming!!! One of them is ReplyContent and one of the is only content.
            });
        }
    }
}
