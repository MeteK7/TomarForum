using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumViewModel.HomeViewModels;

namespace TomarForumBLL.Interfaces
{
    public interface IHomeBLL
    {
        ActionResult<HomeIndexViewModel> GetHomeIndexViewModel();
        string SendEmail(HomeContactViewModel homeContactViewModel);
    }
}
