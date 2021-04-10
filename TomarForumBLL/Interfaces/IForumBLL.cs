using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TomarForumData.EntityModels;
using TomarForumViewModel.ForumViewModels;
using TomarForumViewModel.PostViewModels;

namespace TomarForumBLL.Interfaces
{
    public interface IForumBLL
    {
        ForumUser GetForumUserNewAmount(ApplicationUser user, Forum forum);
        ForumIndexViewModel GetAllForums();
        ActionResult<ForumEditViewModel> GetForumEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        public NewPostViewModel GetForumById(int id);
        ForumTopicViewModel GetTopic(int id, string searchQuery);
        ForumListViewModel BuildForumListing(Post post);
        ForumListViewModel BuildForumListing(Forum forum);
        Task CreateForum(ForumCreateViewModel forumCreateViewModel, string imageUrl);
        string UploadFile(ForumCreateViewModel forumCreateViewModel);
    }
}
