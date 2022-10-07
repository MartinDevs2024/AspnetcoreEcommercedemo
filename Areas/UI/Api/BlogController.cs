using AspnetcoreEcommercedemo.Interfaces;
using AspnetcoreEcommercedemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.UI.Api
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _repo;

        public BlogController(IBlogRepository repo)
        {
            _repo = repo;
        }

        // GET API/ALL
        [HttpGet]
        public ActionResult<Blog> Blogs()
        {
            var blogs = _repo.GetAllBlogs();
            return Ok(blogs);
        }

        // GET API/ID
        [HttpGet("{id}")]
        public ActionResult<Blog> GetBlog(int id)
        {
            var myblog =_repo.GetBlog(id);
            return Ok(myblog);
        }

        // Create API/Blog
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _repo.AddBlog(blog);
            await _repo.SaveChangeAsync();
            return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var myblog = _repo.GetBlog(id);
            if (myblog == null)
                return NotFound();
            _repo.RemoveBlog(id);
            _repo.SaveChangeAsync();
            return Ok(myblog);
        
        }

    }
}
