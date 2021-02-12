﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumUI.ViewModels.ForumViewModels;

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
            var forums = _forumService.GetAll()
                .Select(forum=>new ForumListViewModel {
                    Id=forum.Id,
                    Name=forum.Title,
                    Description=forum.Description
                });
            return View();
        }
    }
}
