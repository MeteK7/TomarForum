using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel.ForumViewModels;

namespace TomarForumBLL
{
    public class ForumBLL: IForumBLL
    {
        private readonly IForumService _forumService;
        public ForumBLL(IForumService forumService)
        {
            _forumService = forumService;
        }
        public ForumUser GetForumUserNewAmount(ApplicationUser user, Forum forum)
        {
            forum.AmountTotalUser += 1;

            return new ForumUser
            {
                User = user,
                Forum = forum
            };
        }

        public ForumIndexViewModel GetAllForums()
        {
            var forums = _forumService.GetAll()
                .Select(forum => new ForumListViewModel
                {
                    Id = forum.Id,
                    Title = forum.Title,
                    Description = forum.Description,
                    ImageUrl = forum.ImageUrl,
                    AmountTotalPost = forum.AmountTotalPost,
                    AmountTotalUser = forum.AmountTotalUser
                });

            var model = new ForumIndexViewModel
            {
                ForumList = forums
            };
            return model;
        }
    }
}
