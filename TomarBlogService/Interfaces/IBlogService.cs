using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TomarBlogData.Models;

namespace TomarBlogService.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> Add(Blog blog);
    }
}
