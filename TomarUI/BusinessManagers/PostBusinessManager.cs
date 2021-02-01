using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TomarDAL.Entities;
using TomarService.Interfaces;
using TomarUI.Authorization;
using TomarUI.BusinessManagers.Interfaces;
using TomarUI.ViewModels.HomeViewModels;
using TomarUI.ViewModels.PostViewModels;

namespace TomarUI.BusinessManagers
{
    public class PostBusinessManager: IPostBusinessManager
    {
        string headerImage = "HeaderImage.jpg";
        string leftSideImage = "LeftSideImage.jpg";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostService postService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAuthorizationService authorizationService;

        public PostBusinessManager(UserManager<ApplicationUser> userManager, IPostService postService, IWebHostEnvironment webHostEnvironment, IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.postService = postService;
            this.webHostEnvironment = webHostEnvironment;
            this.authorizationService = authorizationService;
        }

        public IndexViewModel GetIndexViewModel(string searchString, int? page)
        {
            int pageSize = 20;//Every page, there will be this amount of posts. You can change it to whatever number you want. 
            int pageNumber = page ?? 1;
            var posts = postService.GetPosts(searchString ?? string.Empty)
                .Where(post => post.Published && post.Approved);

            return new IndexViewModel
            {
                Posts = new StaticPagedList<Post>(posts.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, posts.Count()),//TRY TO UNDERSTAND THIS EQUATION.!
                SearchString = searchString,
                PageNumber = pageNumber
            };
        }

        public async Task<ActionResult<PostViewModel>> GetPostViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
            {
                return new BadRequestResult();
            }

            var postId = id.Value;

            var post = postService.GetPost(postId);

            if (post is null)
            {
                return new NotFoundResult();
            }

            if (!post.Published)
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Read);

                if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);
            }

            return new PostViewModel
            {
                Post = post
            };
        }

        public async Task<Post> CreatePost(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Post post = createViewModel.Post;

            post.Creator = await userManager.GetUserAsync(claimsPrincipal);
            post.CreatedOn = DateTime.Now;
            post.UpdatedOn = DateTime.Now;

            post = await postService.Add(post);

            CreateImageInCreate(createViewModel, post.Id, headerImage);
            CreateImageInCreate(createViewModel, post.Id, leftSideImage);

            return post;
        }

        public async Task<ActionResult<Comment>> CreateComment(PostViewModel postViewModel, ClaimsPrincipal claimsPrincipal)
        {
            if (postViewModel.Post is null || postViewModel.Post.Id == 0)
                return new BadRequestResult();

            var post = postService.GetPost(postViewModel.Post.Id);

            if (post is null)
                return new NotFoundResult();

            var comment = postViewModel.Comment;

            comment.Author = await userManager.GetUserAsync(claimsPrincipal);
            comment.Post = post;
            comment.CreatedOn = DateTime.Now;

            if (comment.Parent != null)
            {
                comment.Parent = postService.GetComment(comment.Parent.Id);
            }

            return await postService.Add(comment);
        }

        public async Task<ActionResult<EditViewModel>> UpdatePost(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var post = postService.GetPost(editViewModel.Post.Id);

            if (post is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);

            post.Published = editViewModel.Post.Published;
            post.Title = editViewModel.Post.Title;
            post.Content = editViewModel.Post.Content;
            post.UpdatedOn = DateTime.Now;

            if (editViewModel.HeaderImage != null)
            {
                CreateImageInEdit(editViewModel, post.Id, headerImage);
            }

            if (editViewModel.LeftSideImage != null)
            {
                CreateImageInEdit(editViewModel, post.Id, leftSideImage);
            }

            return new EditViewModel
            {
                Post = await postService.Update(post)
            };
        }

        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
            {
                return new BadRequestResult();
            }

            var postId = id.Value;

            var post = postService.GetPost(postId);

            if (post is null)
            {
                return new NotFoundResult();
            }

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);

            return new EditViewModel
            {
                Post = post
            };
        }

        private ActionResult DetermineActionResult(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }

        private void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (directoryName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }
        private async void CreateImageInCreate(CreateViewModel createViewModel, int postId, string imageName)
        {
            string webRootPath = webHostEnvironment.WebRootPath;
            string pathToImage = $@"{webRootPath}\UserFiles\Posts\{postId}\{imageName}";

            EnsureFolder(pathToImage);//Ensuring that the file is exist or not.

            using (var fileStream = new FileStream(pathToImage, FileMode.Create))
            {
                if (imageName == headerImage)
                    await createViewModel.HeaderImage.CopyToAsync(fileStream);

                else
                    await createViewModel.LeftSideImage.CopyToAsync(fileStream);
            }
        }
        private async void CreateImageInEdit(EditViewModel editViewModel, int postId, string imageName)
        {
            string webRootPath = webHostEnvironment.WebRootPath;
            string pathToImage = $@"{webRootPath}\UserFiles\Posts\{postId}\{imageName}";

            EnsureFolder(pathToImage);//Ensuring that the file is exist or not.

            using (var fileStream = new FileStream(pathToImage, FileMode.Create))
            {
                if (imageName == leftSideImage)
                    await editViewModel.HeaderImage.CopyToAsync(fileStream);
                else
                    await editViewModel.LeftSideImage.CopyToAsync(fileStream);
            }
        }
    }
}
