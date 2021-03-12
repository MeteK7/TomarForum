using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel.ForumViewModels;
using TomarForumViewModel.PostViewModels;
using TomarForumViewModel.SearchViewModels;

namespace TomarForumBLL
{
    public class SearchBLL: ISearchBLL
    {
        private readonly IPostService _postService;
        public SearchBLL(IPostService postService)
        {
            _postService = postService;
        }
        public SearchResultViewModel GetResult(string searchQuery)
        {
            var posts = _postService.GetFilteredPosts(searchQuery);
            var isNoResult = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListings = posts.Select(post => new PostListViewModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.DateCreated.ToString(),
                ReplyAmount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var searchResult = new SearchResultViewModel
            {
                Posts = postListings,
                SearchQuery = searchQuery,
                EmptySearchResult = isNoResult
            };

            return searchResult;
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
    }
}
