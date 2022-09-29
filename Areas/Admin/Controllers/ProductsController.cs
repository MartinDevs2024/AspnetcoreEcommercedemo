using AspnetcoreEcommercedemo.Data;
using AspnetcoreEcommercedemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _he;

        public ProductsController(ApplicationDbContext context, IHostingEnvironment he)
        {
            _context = context;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(c =>c.ProductTypes).Include(f=>f.SpecialTag).ToList());
        }



        //Create method
        public IActionResult Create() {

            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_context.SpecialTags.ToList(), "Id", "Name");

            return View();
        }

        // Create method
        [HttpPost]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (ModelState.IsValid) 
            {
                var searchProduct = _context.Products.FirstOrDefault(c => c.Name == products.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already listed";
                    ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_context.SpecialTags.ToList(), "Id", "Name");
                    return View(products);
                }

                if (image != null) 
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "Images/noimage.PNG";
                }

                _context.Products.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // Edit method 
        public IActionResult Edit(int? id)
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_context.SpecialTags.ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
           
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "Images/noimage.PNG";
                }

                _context.Products.Update(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // Detail action method
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //Get Delete Action Method
        public IActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).Where(c => c.Id == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //Post Delete Action Method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(c=>c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
