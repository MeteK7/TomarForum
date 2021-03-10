﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel;
using TomarForumViewModel.ForumViewModels;
using TomarForumViewModel.HomeViewModels;
using TomarForumViewModel.PostViewModels;

namespace TomarForumUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly IHomeBLL _homeBLL;
        public HomeController(IPostService postService, IHomeBLL homeBLL)
        {
            _postService = postService;
            _homeBLL = homeBLL;
        }

        public IActionResult Index()
        {
            var model = _homeBLL.GetHomeIndexViewModel();

            return View(model.Value);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
