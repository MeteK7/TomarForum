using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TomarData.Models;

namespace TomarUI.Models.PostViewModels
{
    public class CreateViewModel
    {
        [Display(Name = "Left Side Image")]
        public IFormFile LeftSideImage { get; set; }

        [Required, Display(Name ="Header Image")]
        public IFormFile HeaderImage { get; set; }

        public Post Post { get; set; }
    }
}
