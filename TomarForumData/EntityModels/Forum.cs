﻿using System;
using System.Collections.Generic;

namespace TomarForumData.EntityModels
{
    public class Forum
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string ImageUrl { get; set; }
        public int AmountTotalPost { get; set; }
        public int AmountTotalUser { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }

    }
}
