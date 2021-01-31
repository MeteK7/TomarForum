using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TomarData.Models;

namespace TomarService.Interfaces
{
    public interface IUserService
    {
        ApplicationUser Get(string id);
        Task<ApplicationUser> Update(ApplicationUser applicationUser);
    }
}
