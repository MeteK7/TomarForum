using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumViewModel.PostViewModels;

namespace TomarForumViewModel.ForumViewModels
{
    public class ForumTopicViewModel
    {
        public ForumListViewModel Forum { get; set; }
        public IEnumerable<PostListViewModel> Posts { get; set; }
        public string SearchQuery { get; set; }
    }
}
