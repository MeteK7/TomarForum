using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumUI.ViewModels.PostViewModels;

namespace TomarForumUI.ViewModels.SearchViewModels
{
    public class SearchResultViewModel
    {
        public IEnumerable<PostListViewModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResult { get; set; }//Results or only result?
    }
}
