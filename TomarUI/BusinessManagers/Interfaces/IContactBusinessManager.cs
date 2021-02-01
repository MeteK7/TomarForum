using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarUI.ViewModels.ContactViewModels;

namespace TomarUI.BusinessManagers.Interfaces
{
    public interface IContactBusinessManager
    {
        public string SendEmail(SendEmailViewModel sendEmailViewModel);
    }
}
