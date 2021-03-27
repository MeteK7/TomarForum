using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumData.EntityModels;

namespace TomarForumService.Interfaces
{
    public interface IPostService
    {
        Post GetById(int postId);
        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetPostsByUser(ApplicationUser applicationUser);
        IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery);
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLatestPosts(int v);//Change the v!!!
    }
}
