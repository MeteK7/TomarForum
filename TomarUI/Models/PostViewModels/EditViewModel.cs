using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TomarData.Models;

namespace TomarUI.Models.PostViewModels
{
    public class EditViewModel
    {
        [Display(Name = "Left Side Image")]
        public IFormFile LeftSideImage { get; set; }

        [Display(Name = "Header Image")]
        public IFormFile HeaderImage { get; set; }

        public Post Post { get; set; }
    }
}
