using AspnetcoreEcommercedemo.Models;
using AspnetcoreEcommercedemo.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Interfaces
{
    public interface IBlogRepository
    {
        Blog GetBlog(int id);
        List<Blog> GetAllBlogs();
        void AddBlog(Blog blog);
        void UpdateBlog(Blog blog);
        void RemoveBlog(int id);
        void AddSubComment(SubComment comment);
        Task<bool> SaveChangeAsync();

    }
}
