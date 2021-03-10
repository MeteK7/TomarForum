using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel.ForumViewModels;
using TomarForumViewModel.PostViewModels;

namespace TomarForumUI.Controllers
{
    public class ForumController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IForumService _forumService;
        private readonly IPostService _postService;
        private readonly IForumBLL _forumBLL;
        public ForumController(IForumService forumService, IPostService postService, IWebHostEnvironment webHostEnvironment, IForumBLL forumBLL)
        {
            _forumService = forumService;
            _postService = postService;
            _webHostEnvironment = webHostEnvironment;
            _forumBLL = forumBLL;
        }

        public IActionResult Index()
        {
            var allForums = _forumBLL.GetAllForums();

            return View(allForums);
        }

        public IActionResult Topic(int id, string searchQuery)/*FIX THE CODE ERROR BELOW!*/
        {
            var forum = _forumService.GetById(id);
            var posts = new List<Post>();

            posts = _postService.GetFilteredPosts(forum, searchQuery).ToList(); //forum.Posts;

            var postListings = posts.Select(post => new PostListViewModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                AuthorName=post.User.UserName, //Fix the naming
                Title = post.Title,
                DatePosted = post.DateCreated.ToString(),
                ReplyAmount=post.Replies.Count(),
                Forum=BuildForumListing(post)
            });

            var model = new ForumTopicViewModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(model);

            //THIS CODE GIVES NULL ERROR
            //var topic = _forumBLL.GetTopic(id,searchQuery);

            //return View(topic);
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

        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }

        public IActionResult Create()
        {
            return View(new ForumCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ForumCreateViewModel forumCreateViewModel)
        {
            string imageUrl = UploadFile(forumCreateViewModel);

            var forum = new TomarForumData.EntityModels.Forum()
            {
                Title = forumCreateViewModel.Title,
                Description = forumCreateViewModel.Description,
                Created = DateTime.Now,
                ImageUrl = imageUrl,
                AmountTotalPost=forumCreateViewModel.AmountTotalPost,
                AmountTotalUser=forumCreateViewModel.AmountTotalUser
            };

            await _forumService.Add(forum);
            return RedirectToAction("Index","Forum");
        }

        private string UploadFile(ForumCreateViewModel forumCreateViewModel)
        {
            string filePath = "", dataBasePath="";
            if (forumCreateViewModel!=null)
            {
                string mainPath=Path.Combine(_webHostEnvironment.WebRootPath,@"images\forums");
                string fileName = Guid.NewGuid().ToString() + "-" + forumCreateViewModel.ImageUpload.FileName;
                filePath = Path.Combine(mainPath, fileName);
                using (var fileStream=new FileStream(filePath,FileMode.Create))
                {
                    forumCreateViewModel.ImageUpload.CopyTo(fileStream);
                }
                dataBasePath= $@"../../images/forums/{fileName}";
            }
            return dataBasePath;
        }
    }
}
