using Microsoft.EntityFrameworkCore;
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
    public class ForumService : IForumService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ForumService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _applicationDbContext.Forums//Getting every instance of a forum from the database.
                .Include(forum=>forum.Posts);  //In order to load the virtual posts navigation property explicitly, we need to call "Include".
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
            var forum = _applicationDbContext.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts).ThenInclude(p => p.User)
                .Include(f => f.Posts).ThenInclude(p => p.Replies).ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
        }

        public bool CheckUserFirstPostByForum(string userId, int forumId)
        {
            var userForumRecord = _applicationDbContext.ForumUsers
                .Where(fUser => fUser.User.Id == userId && fUser.Forum.Id == forumId)
                .Include(fUser=>fUser.Forum)
                .Include(fUser=>fUser.User)
                .FirstOrDefault();
            bool result;

            if (userForumRecord!=null && userForumRecord.Id != null)//A user ID cannot be 0 so that it indicates that if a user exists.
            {
                result = false;//If user has a post for a specific forum, then no need to increase the total forum amount.
            }
            else//Do you really need an else statement??
            {
                result = true;
            }

            return result;
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Forum forum)
        {
            _applicationDbContext.Add(forum);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Forum> Update(Forum forum)
        {
            _applicationDbContext.Update(forum);
            await _applicationDbContext.SaveChangesAsync();
            return forum;
        }

        public async Task AddNewUser(ForumUser forumUser)
        {
            _applicationDbContext.Add(forumUser);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
