using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel.ForumViewModels;
using TomarForumViewModel.HomeViewModels;
using TomarForumViewModel.PostViewModels;

namespace TomarForumBLL
{
    public class HomeBLL:IHomeBLL
    {
        private readonly IPostService _postService;

        public HomeBLL(IPostService postService)
        {
            _postService = postService;
        }

        public ActionResult<HomeIndexViewModel> GetHomeIndexViewModel()
        {
            var latestPosts = _postService.GetLatestPosts(22);

            var posts = latestPosts.Select(post => new PostListViewModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                DatePosted = post.DateCreated.ToString(),
                ReplyAmount = post.Replies.Count(),
                Forum = GetForumListingForPost(post)
            });

            return new HomeIndexViewModel
            {
                LatestPosts = posts,
                SearchQuery = ""
            };
        }

        private ForumListViewModel GetForumListingForPost(Post post)
        {
            var forum = post.Forum;

            return new ForumListViewModel
            {
                Id = forum.Id,
                Title = forum.Title,
                ImageUrl = forum.ImageUrl
            };
        }
    }
}
