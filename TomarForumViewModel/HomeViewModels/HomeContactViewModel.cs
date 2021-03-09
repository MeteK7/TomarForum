using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomarForumViewModel.HomeViewModels
{
    public class HomeContactViewModel
    {
        public string To { get; set; }
        [Display(Name = "Your name:")]
        public string GuestFirstName { get; set; }
        [Display(Name = "Your surname:")]
        public string GuestLastName { get; set; }
        [Display(Name = "Phone number:")]
        public string GuestPhoneNumber { get; set; }
        [Display(Name = "Email:")]
        public string GuestEmailAddress { get; set; }
        [Display(Name = "Subject:")]
        public string Subject { get; set; }
        [Display(Name = "Body:")]
        public string Body { get; set; }
        [Display(Name = "Attachment:")]
        public List<IFormFile> Attachment { get; set; }
    }
}
