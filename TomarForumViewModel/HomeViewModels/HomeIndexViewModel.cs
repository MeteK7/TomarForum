using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumViewModel.PostViewModels;

namespace TomarForumViewModel.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListViewModel> LatestPosts { get; set; }
    }
}
