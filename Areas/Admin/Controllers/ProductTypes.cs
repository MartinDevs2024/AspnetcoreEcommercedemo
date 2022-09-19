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
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.ProductTypes.ToList());
        }

        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _context.ProductTypes.Add(productTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(productTypes);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Update(productTypes);
                await _context.SaveChangesAsync();
                TempData["Edit"] = "Product type has been updated";
                return RedirectToAction(nameof(Index));
         
            }
            return View(productTypes);
        
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
                return NotFound();
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != productTypes.Id)
            {
                return NotFound();
            }
            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Remove(productType);
                await _context.SaveChangesAsync();
                TempData["delete"] = "Product Type has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }
    }
}
