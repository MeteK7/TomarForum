using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumData.EntityModels;

namespace TomarForumViewModel.PostViewModels
{
    public class PostEditViewModel
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
    }
}
