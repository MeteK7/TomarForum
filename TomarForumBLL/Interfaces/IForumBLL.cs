using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumData.EntityModels;

namespace TomarForumBLL.Interfaces
{
    public interface IForumBLL
    {
        ForumUser InsertForumUserAmount(ApplicationUser user, Forum forum);
    }
}
