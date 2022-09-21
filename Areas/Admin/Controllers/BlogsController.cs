using AspnetcoreEcommercedemo.Interfaces;
using AspnetcoreEcommercedemo.Models;
using AspnetcoreEcommercedemo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly IBlogRepository _repo;
        private readonly IFileManager _fileManager;

        public BlogsController(IBlogRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id) 
        {
            return View(_repo.GetBlog(id));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new BlogViewModel());
            else
            {
                var blog = _repo.GetBlog((int)id);
                return View(new BlogViewModel
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Body = blog.Body,
                    CurrentImage = blog.Image,
                    Description = blog.Description,
                    Category = blog.Category,
                    Tags = blog.Tags
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogViewModel vm)
        {
            var blog = new Blog
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags
            };
            if (vm.Image == null)
            {
                blog.Image = vm.CurrentImage;
            }
            else
            {
                blog.Image = await _fileManager.SaveImage(vm.Image);
            }

            if (blog.Id > 0)
                _repo.UpdateBlog(blog);
            else
                _repo.AddBlog(blog);
            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(blog);
        }


        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult Image(string image)
        {
            var mine = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mine}");
        }


        #region 
        [HttpGet]
        public IActionResult GetAll()
        {
            var objFromDb = _repo.GetAllBlogs();
            return Json(new { data = objFromDb });
        }



        #endregion
    }
}
