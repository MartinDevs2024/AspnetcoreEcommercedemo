using AspnetcoreEcommercedemo.Data;
using AspnetcoreEcommercedemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialTagsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.SpecialTags.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _context.SpecialTags.Add(specialTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = _context.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _context.SpecialTags.Update(specialTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }

        //Get Details Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = _context.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
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
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = _context.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        //POST Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id, SpecialTag specialTag)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != specialTag.Id)
            {
                return NotFound();
            }

            var specialTags = _context.SpecialTags.Find(id);
            if (specialTags == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Remove(specialTags);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }




    }
}
