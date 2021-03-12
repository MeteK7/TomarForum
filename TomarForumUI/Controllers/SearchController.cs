using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel.ForumViewModels;
using TomarForumViewModel.PostViewModels;
using TomarForumViewModel.SearchViewModels;

namespace TomarForumUI.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPostService _postService;
        private readonly ISearchBLL _searchBLL;
        public SearchController(IPostService postService, ISearchBLL searchBLL)
        {
            _postService = postService;
            _searchBLL = searchBLL;
        }
        public IActionResult Result(string searchQuery)//Should be Result or Results?
        {
            var searchResult = _searchBLL.GetResult(searchQuery);
            return View(searchResult);
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }
    }
}
