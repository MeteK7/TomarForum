using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TomarBlogData.Models;

namespace TomarBlogUI.Models.BlogViewModels
{
    public class CreateViewModel
    {
        [Required, Display(Name ="Header Image")]
        public IFormFile BlogHeaderImage { get; set; }
        public Blog Blog { get; set; }
    }
}
