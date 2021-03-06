using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumData.EntityModels;

namespace TomarForumViewModel.AdminViewModels
{
    public class AdminIndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
