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
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ApplicationUserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _applicationDbContext.ApplicationUsers;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(user => user.Id == id);
        }

        public Task IncrementRating(string id, Type type)
        {
            throw new NotImplementedException();
        }

        public async Task SetProfileImage(string id, Uri uri)
        {
            var user = GetById(id);
            user.ProfileImageUrl = uri.AbsoluteUri;
            _applicationDbContext.Update(user);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
