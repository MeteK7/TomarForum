using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TomarUI.Models;
using TomarUI.BusinessManagers.Interfaces;

namespace TomarUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostBusinessManager postBusinessManager;
        private readonly IHomeBusinessManager homeBusinessManager;
        public HomeController(IPostBusinessManager postBusinessManager, IHomeBusinessManager homeBusinessManager)
        {
            this.postBusinessManager = postBusinessManager;
            this.homeBusinessManager = homeBusinessManager;
        }

        public IActionResult Index(string searchString,int? page)
        {
            return View(postBusinessManager.GetIndexViewModel(searchString, page));
        }

        /*IT MUST BE IN ADMIN CONTROLLER!!!*/
        public IActionResult Author(string authorId, string searchString, int? page) //View Author's about me page in About editing.
        {
            var actionResult = homeBusinessManager.GetAuthorViewModel(authorId, searchString, page);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
