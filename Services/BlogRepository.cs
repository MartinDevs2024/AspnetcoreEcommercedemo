using AspnetcoreEcommercedemo.Data;
using AspnetcoreEcommercedemo.Interfaces;
using AspnetcoreEcommercedemo.Models;
using AspnetcoreEcommercedemo.Models.Comments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Services
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
        }

        public List<Blog> GetAllBlogs()
        {
            return _context.Blogs.ToList();
        }

        public Blog GetBlog(int id)
        {
            return _context.Blogs
                .Include(p => p.MainComments)
                .ThenInclude(m => m.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }

        public void RemoveBlog(int id)
        {
            _context.Blogs.Remove(GetBlog(id));
        }

        public void UpdateBlog(Blog blog)
        {
            _context.Blogs.Update(blog);
        }

        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
