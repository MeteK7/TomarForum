using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumData.EntityModels;

namespace TomarForumViewModel.PostViewModels
{
    public class PostEditViewModel
    {
        public Post Post { get; set; }
        //public int PostId { get; set; }

        //public string PostTitle { get; set; }

        //public string PostContent { get; set; }

        [Display(Name = "Header Image")]
        public IFormFile HeaderImage { get; set; }
    }
}
