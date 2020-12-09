using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TomarBlogData.Models;

namespace TomarBlogService.Interfaces
{
    public interface IUserService
    {
        ApplicationUser Get(string id);
        Task<ApplicationUser> Update(ApplicationUser applicationUser);
    }
}
