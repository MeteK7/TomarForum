using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TomarForumData.EntityModels;

namespace TomarForumData.EntityModels
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Forum Forum { get; set; }
        public virtual IEnumerable<PostReply> Replies { get; set; }
    }
}
