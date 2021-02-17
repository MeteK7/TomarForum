using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumData;
using TomarForumData.EntityModels;
using TomarForumService.Interfaces;

namespace TomarForumService
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PostService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task Add(Post post)
        {
            _applicationDbContext.Add(post);
            await _applicationDbContext.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _applicationDbContext.Posts
                .Include(post => post.User)
                .Include(post => post.Replies)
                    .ThenInclude(reply => reply.User)
                .Include(post => post.Forum);
        }

        public Post GetById(int id)
        {
            return _applicationDbContext.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies)
                    .ThenInclude(reply=>reply.User)
                .Include(post => post.Forum)
                .First();
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? forum.Posts
                : forum.Posts.Where(post => post.Title.Contains(searchQuery) || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetLatestPosts(int v)//Change the variable v!!!
        {
            return GetAll().OrderByDescending(post => post.DateCreated).Take(v);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            var posts = _applicationDbContext.Forums
                .Where(forum => forum.Id == id).First()
                .Posts;

            return posts;
        }
    }
}
