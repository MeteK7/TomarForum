﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
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

        [AllowAnonymous]
        public IActionResult Index()
        {
            var allForums = _forumBLL.GetAllForums();

            return View(allForums);
        }

        [AllowAnonymous]
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
                Forum=_forumBLL.BuildForumListing(post)
            });

            var model = new ForumTopicViewModel
            {
                Posts = postListings,
                Forum = _forumBLL.BuildForumListing(forum)
            };

            return View(model);

            //THIS CODE GIVES NULL ERROR
            //var topic = _forumBLL.GetTopic(id,searchQuery);

            //return View(topic);
        }

        [AllowAnonymous]
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
            string imageUrl = _forumBLL.UploadFile(forumCreateViewModel);

            await _forumBLL.CreateForum(forumCreateViewModel, imageUrl);
            
            return RedirectToAction("Index","Forum");
        }


        public IActionResult Edit(int? id)
        {
            var forum= _forumBLL.GetForumEditViewModel(id,User);

            return View(forum.Value);
        }
    }
}
