using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumData.EntityModels;

namespace TomarForumViewModel.PostViewModels
{
    public class PostUserViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
