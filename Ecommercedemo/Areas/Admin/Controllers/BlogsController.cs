using AspnetcoreEcommercedemo.Interfaces;
using AspnetcoreEcommercedemo.Models;
using AspnetcoreEcommercedemo.Models.ViewModels;
using AspnetcoreEcommercedemo.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BlogsController : Controller
    {
        private readonly IBlogRepository _repo;
        private readonly IFileManager _fileManager;
        private readonly IPhotoService _photoService;

        public BlogsController(IBlogRepository repo, 
            IFileManager fileManager,
            IPhotoService photoService)
        {
            _repo = repo;
            _fileManager = fileManager;
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            var blog = _repo.GetAllBlogs().ToList();
            return View(blog);
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
                  
                    Description = blog.Description,
                    Category = blog.Category,
                    Tags = blog.Tags
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogViewModel vm)
        {
            var blog = new Models.Blog
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags
            };
         
            if (blog.Id > 0)
                _repo.UpdateBlog(blog);
            else
                _repo.AddBlog(blog);
            if (await _repo.SaveChangeAsync())
                return RedirectToAction("Index");
            else
                return View(blog);
        }
        //Adding Photos 
        [HttpPost("add-photo")]
        public async Task<ActionResult<Photo>> AddPhoto(int? id, IFormFile file)
        {
            var blog = _repo.GetBlog((int)id);
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if (blog.Photos.Count == 0) photo.IsMain = true;
            blog.Photos.Add(photo);
            return RedirectToAction("Index");
        }


        #region 
        [HttpGet]
        public IActionResult GetAll()
        {
            var objFromDb = _repo.GetAllBlogs();
            return Json(new { data = objFromDb });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = _repo.GetBlog(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _repo.RemoveBlog(id);
            await _repo.SaveChangeAsync();
            return Json(new { success = true, message = "Delete Successfully" });

        }

        #endregion
    }
}
