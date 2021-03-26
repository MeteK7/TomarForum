using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel.ForumViewModels;
using TomarForumViewModel.PostViewModels;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace TomarForumBLL
{
    public class ForumBLL: IForumBLL
    {
        private readonly IForumService _forumService;
        private readonly IPostService _postService;
        private IHostingEnvironment _environment;

        public ForumBLL(IForumService forumService, IPostService postService, IHostingEnvironment environment)
        {
            _forumService = forumService;
            _postService = postService;
            _environment = environment;
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

        public ForumListViewModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }

        public ForumListViewModel BuildForumListing(Forum forum)
        {
            return new ForumListViewModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }

        public async Task CreateForum(ForumCreateViewModel forumCreateViewModel, string imageUrl)
        {
            var forum = new Forum()
            {
                Title = forumCreateViewModel.Title,
                Description = forumCreateViewModel.Description,
                Created = DateTime.Now,
                ImageUrl = imageUrl,
                AmountTotalPost = forumCreateViewModel.AmountTotalPost,
                AmountTotalUser = forumCreateViewModel.AmountTotalUser
            };

            await _forumService.Add(forum);
        }

        public ActionResult<ForumEditViewModel> GetForumEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
            {
                return new BadRequestResult();
            }

            var forumId = id.Value;

            var forum = _forumService.GetById(forumId);

            if (post is null)
            {
                return new NotFoundResult();
            }

            else
            {
                return new ForumEditViewModel
                {
                    Forum = forum,
                };
            }

        }

        public string UploadFile(ForumCreateViewModel forumCreateViewModel)
        {
            string filePath, dataBasePath="";
            if (forumCreateViewModel != null)
            {
                string mainPath = Path.Combine(_environment.WebRootPath, @"images\forums");
                string fileName = Guid.NewGuid().ToString() + "-" + forumCreateViewModel.ImageUpload.FileName;
                filePath = Path.Combine(mainPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    forumCreateViewModel.ImageUpload.CopyTo(fileStream);
                }
                dataBasePath = $@"../../images/forums/{fileName}";
            }
            return dataBasePath;
        }
    }
}
