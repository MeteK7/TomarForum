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
        public ForumUser GetForumUserNewAmount(ApplicationUser user, Forum forum)
        {
            forum.AmountTotalUser += 1;

            return new ForumUser
            {
                User = user,
                Forum = forum
            };
        }
    }
}
