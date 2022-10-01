using AspnetcoreEcommercedemo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.UI.Controllers
{
    [Area("UI")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _repo;
        public BlogController(IBlogRepository repo)
        {
            _repo = repo;
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
