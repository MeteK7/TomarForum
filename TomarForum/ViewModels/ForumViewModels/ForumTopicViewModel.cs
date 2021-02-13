using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumUI.ViewModels.PostViewModels;

namespace TomarForumUI.ViewModels.ForumViewModels
{
    public class ForumTopicViewModel
    {
        public ForumListViewModel Forum { get; set; }
        public IEnumerable<PostListViewModel> Posts { get; set; }
    }
}
