using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomarBlogUI.Models.ContactViewModels
{
    public class SendEmailViewModel
    {
        public string To { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public string GuestPhoneNumber { get; set; }
        public string GuestEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
