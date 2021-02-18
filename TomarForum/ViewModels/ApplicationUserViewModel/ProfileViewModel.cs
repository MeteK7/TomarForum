﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomarForumUI.ViewModels.ApplicationUserViewModel
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserRating { get; set; }
        public string profileImageUrl { get; set; }
        public DateTime MembershipCreatedOn { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}