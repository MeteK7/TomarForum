﻿using Microsoft.AspNetCore.Http;
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
    }
}
