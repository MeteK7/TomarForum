using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TomarBlogData.Models;

namespace TomarBlogService.Interfaces
{
    public interface IBlogService
    {
        Blog GetBlog(int blogId);
        IEnumerable<Blog> GetBlogs(string searchString);//Ordering by descending
        IEnumerable<Blog> GetBlogs(ApplicationUser applicationUser);
        Task<Blog> Add(Blog blog);
        Task<Blog> Update(Blog blog);
    }
}
