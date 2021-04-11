using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TomarForumBLL.Interfaces;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;
using TomarForumViewModel.ForumViewModels;
using TomarForumViewModel.HomeViewModels;
using TomarForumViewModel.PostViewModels;

namespace TomarForumBLL
{
    public class HomeBLL:IHomeBLL
    {
        private readonly IPostService _postService;

        public HomeBLL(IPostService postService)
        {
            _postService = postService;
        }

        public ActionResult<HomeIndexViewModel> GetHomeIndexViewModel()
        {
            var latestPosts = _postService.GetLatestPosts(22);

            var posts = latestPosts.Select(post => new PostListViewModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                DatePosted = post.DateCreated.ToString(),
                ReplyAmount = post.Replies.Count(),
                Forum = GetForumListingForPost(post)
            });

            return new HomeIndexViewModel
            {
                LatestPosts = posts,
                SearchQuery = ""
            };
        }
        private ForumListViewModel GetForumListingForPost(Post post)
        {
            var forum = post.Forum;

            return new ForumListViewModel
            {
                Id = forum.Id,
                Title = forum.Title,
                ImageUrl = forum.ImageUrl
            };
        }

        public string SendEmail(HomeContactViewModel homeContactViewModel)
        {
            string ownerEmail = "fake0941@gmail.com";
            string guestFirstName = homeContactViewModel.GuestFirstName;
            string guestLastName = homeContactViewModel.GuestLastName;
            string guestPhoneNumber = homeContactViewModel.GuestPhoneNumber;
            string guestEmail = homeContactViewModel.GuestEmailAddress;
            string emailSubject = homeContactViewModel.Subject;
            string emailBody = homeContactViewModel.Body;
            string emailFullBody = "Guest's\n\nEmail: " + guestEmail + "\nName: " + guestFirstName + "\nSurname: " + guestLastName + "\nPhone Number: " + guestPhoneNumber + "\n\n\n" + emailBody;
            string deliveryStatus;

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.To.Add(ownerEmail);
                mailMessage.From = new MailAddress(ownerEmail);
                mailMessage.Subject = emailSubject;
                mailMessage.Body = emailFullBody;
                mailMessage.IsBodyHtml = false;

                if (homeContactViewModel.Attachment != null)
                {
                    foreach (IFormFile attachedFile in homeContactViewModel.Attachment)
                    {
                        string fileName = Path.GetFileName(attachedFile.FileName);
                        mailMessage.Attachments.Add(new Attachment(attachedFile.OpenReadStream(), fileName));
                    }
                }

                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Port = 587;
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential("fake0941@gmail.com", "aszx323+776");
                    smtpClient.Send(mailMessage);
                }
            }

            deliveryStatus = "Mail has been sent successfully.";

            return deliveryStatus;
        }


    }
}
