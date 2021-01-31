using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarUI.BusinessManagers.Interfaces;
using TomarUI.Models.ContactViewModels;

namespace TomarUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactBusinessManager contactBusinessManager;

        public ContactController(IContactBusinessManager contactBusinessManager)
        {
            this.contactBusinessManager = contactBusinessManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(SendEmailViewModel sendEmailViewModel)
        {

            ViewBag.Message = contactBusinessManager.SendEmail(sendEmailViewModel);

            return RedirectToAction("Index");
        }
    }
}
