using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarBlogData.Models;

namespace TomarBlogUI.Models.AdminViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
