using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarBlogUI.Models.ContactViewModels;

namespace TomarBlogUI.BusinessManagers.Interfaces
{
    public interface IContactBusinessManager
    {
        public string SendEmail(SendEmailViewModel sendEmailViewModel);
    }
}
