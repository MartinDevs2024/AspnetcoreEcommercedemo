using AspnetcoreEcommercedemo.DataAccess.Data;
using AspnetcoreEcommercedemo.DataAccess.Repository.IRepository;
using AspnetcoreEcommercedemo.Models;
using AspnetcoreEcommercedemo.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SpecialTagsController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public SpecialTagsController(IUnitOfWork unitOfWork)
        {
     
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<SpecialTag> objSpecialTagsList = _unitOfWork.SpecialTags.GetAll();
            return View(objSpecialTagsList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SpecialTag obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SpecialTags.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }
        //GET
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var specialTagFromDbFirst = _unitOfWork.SpecialTags
                .GetFirstOrDefault(u =>u.Id == id);
            if (specialTagFromDbFirst == null)
            {
                return NotFound();
            }
            return View(specialTagFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SpecialTag obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SpecialTags.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        //Get Details Action Method
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var specialTagFromDbFirst = _unitOfWork.SpecialTags
                .GetFirstOrDefault(u =>u.Id == id);
            if (specialTagFromDbFirst == null)
            {
                return NotFound();
            }
            return View(specialTagFromDbFirst);
        }

        //Post Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTag specialTag)
        {
            return RedirectToAction(nameof(Index));
        }

        //GET Delete Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var specialTagFromDbFirst = _unitOfWork.SpecialTags
                .GetFirstOrDefault(u =>u.Id == id);
            if (specialTagFromDbFirst == null)
            {
                return NotFound();
            }
            return View(specialTagFromDbFirst);
        }
        //POST Delete Action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var specialTag = _unitOfWork.SpecialTags
                .GetFirstOrDefault(u =>u.Id==id);
            if (specialTag == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.SpecialTags.Remove(specialTag);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }
    }
}
