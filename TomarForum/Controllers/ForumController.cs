using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;

namespace TomarForumUI.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            IEnumerable<Forum> forums = _forumService.GetAll();
            return View();
        }
    }
}
