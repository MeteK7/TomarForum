﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarForumViewModel.ForumViewModels;

namespace TomarForumViewModel.PostViewModels
{
    public class PostListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }
        public string DatePosted { get; set; }
        //public int ForumId { get; set; }
        //public string ForumImageUrl { get; set; }
        //public string ForumName { get; set; }
        public ForumListViewModel Forum { get; set; }
        public int ReplyAmount { get; set; }
    }
}
