﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomarForumData.EntityModels
{
    public class ForumUser
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Forum Forum { get; set; }
    }
}
