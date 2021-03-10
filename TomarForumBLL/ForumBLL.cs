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
using TomarForumViewModel.PostViewModels;

namespace TomarForumBLL
{
    public class ForumBLL: IForumBLL
    {
        private readonly IForumService _forumService;
        private readonly IPostService _postService;
        public ForumBLL(IForumService forumService, IPostService postService)
        {
            _forumService = forumService;
        }
        public ForumUser GetForumUserNewAmount(ApplicationUser user, Forum forum)
        {
            forum.AmountTotalUser += 1;

            return new ForumUser
            {
                User = user,
                Forum = forum
            };
        }

        public ForumIndexViewModel GetAllForums()
        {
            var forums = _forumService.GetAll()
                .Select(forum => new ForumListViewModel
                {
                    Id = forum.Id,
                    Title = forum.Title,
                    Description = forum.Description,
                    ImageUrl = forum.ImageUrl,
                    AmountTotalPost = forum.AmountTotalPost,
                    AmountTotalUser = forum.AmountTotalUser
                });

            var model = new ForumIndexViewModel
            {
                ForumList = forums
            };
            return model;
        }

        public ForumTopicViewModel GetTopic(int id,string searchQuery)
        {
            var forum = _forumService.GetById(id);
            var posts = new List<Post>();

            posts = _postService.GetFilteredPosts(forum, searchQuery).ToList(); //forum.Posts;

            var postListings = posts.Select(post => new PostListViewModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                AuthorName = post.User.UserName, //Fix the naming
                Title = post.Title,
                DatePosted = post.DateCreated.ToString(),
                ReplyAmount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new ForumTopicViewModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return model;
        }

        private ForumListViewModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }

        private ForumListViewModel BuildForumListing(Forum forum)
        {
            return new ForumListViewModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }
    }
}
