using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumUI.ViewModels.ForumViewModels;
using TomarForumUI.ViewModels.PostViewModels;
using TomarForumUI.ViewModels.SearchViewModels;

namespace TomarForumUI.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPostService _postService;
        public SearchController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Result(string searchQuery)//Should be Result or Results?
        {
            var posts = _postService.GetFilteredPosts(searchQuery);
            var isNoResult = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());
            var postListings = posts.Select(post => new PostListViewModel
            {
                Id=post.Id,
                AuthorId=post.User.Id,
                AuthorName=post.User.UserName,
                AuthorRating=post.User.Rating,
                Title=post.Title,
                DatePosted=post.DateCreated.ToString(),
                ReplyAmount=post.Replies.Count(),
                Forum=BuildForumListing(post)
            });

            var model = new SearchResultViewModel
            {
                Posts=postListings,
                SearchQuery=searchQuery,
                EmptySearchResult= isNoResult
            };
            return View(model);
        }

        private ForumListViewModel BuildForumListing(Post post)
        {
            var forum = post.Forum;

            return new ForumListViewModel
            {
                Id = forum.Id,
                ImageUrl = forum.ImageUrl,
                Title = forum.Title,
                Description = forum.Description
            };
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }
    }
}
