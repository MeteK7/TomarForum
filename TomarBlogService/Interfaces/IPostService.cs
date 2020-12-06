using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TomarBlogData.Models;

namespace TomarBlogService.Interfaces
{
    public interface IPostService
    {
        Post GetPost(int postId);
        IEnumerable<Post> GetPosts(string searchString);//Ordering by descending
        IEnumerable<Post> GetPosts(ApplicationUser applicationUser);
        Task<Post> Add(Post post);
        Task<Post> Update(Post post);
    }
}
