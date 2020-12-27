using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomarBlogUI.Models.ContactViewModels
{
    public class SendEmailViewModel
    {
        public string To { get; set; }
        [Display(Name="Let your name be known:")]
        public string GuestFirstName { get; set; }
        [Display(Name ="Your surname:")]
        public string GuestLastName { get; set; }
        [Display(Name = "May I get your number down here?")]
        public string GuestPhoneNumber { get; set; }
        [Display(Name = "Email please:")]
        public string GuestEmailAddress { get; set; }
        [Display(Name = "Briefly:")]
        public string Subject { get; set; }
        [Display(Name = "What is it about?:")]
        public string Body { get; set; }
        [Display(Name = "Anything additional?")]
        public List<IFormFile> Attachment { get; set; }
    }
}
