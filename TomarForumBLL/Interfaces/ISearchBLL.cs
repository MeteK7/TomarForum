using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumViewModel.SearchViewModels;

namespace TomarForumBLL.Interfaces
{
    public interface ISearchBLL
    {
        SearchResultViewModel GetResult(string searchQuery);
    }
}
