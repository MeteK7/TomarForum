using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;

namespace TomarForumBLL
{
    public class ForumBLL: IForumBLL
    {
        public ForumUser InsertForumUserAmount(ApplicationUser user, Forum forum)
        {
            return new ForumUser
            {
                User = user,
                Forum = forum
            };
        }
    }
}
