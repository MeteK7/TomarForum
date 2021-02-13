using System;
using TomarForumData.EntityModels;

namespace TomarForumData.EntityModels
{
    public class PostReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ApplicationUser User{ get; set; }
        public virtual Post Post { get; set; }
    }
}
