using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumData.EntityModels;

namespace TomarForumService.Interfaces
{
    public interface IForumService
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();
        bool CheckUserFirstPostByForum(string userId, int forumId);
        Task Create(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
        Task<Forum> Update(Forum forum);
        Task Add(Forum forum);
        Task AddNewUser(ForumUser forumUser);
    }
}
