using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;

namespace TomarForumUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminBLL _adminBLL;
        public AdminController(IAdminBLL adminBLL)
        {
            _adminBLL = adminBLL;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _adminBLL.GetAdminDashboard(User));
        }
    }
}