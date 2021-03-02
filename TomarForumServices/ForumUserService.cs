using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumData;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;

namespace TomarForumService
{
    public class ForumUserService: IForumUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ForumUserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task Add(ForumUser forumUser)
        {
            _applicationDbContext.Add(forumUser);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
