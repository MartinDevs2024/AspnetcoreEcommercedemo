
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
    public class ProductTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductTypesController(IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<ProductTypes> objProductTypesList = _unitOfWork.ProductTypes.GetAll();
            return View(objProductTypesList);
        }

        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductTypes obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductTypes.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Types created successfully";
                return RedirectToAction(actionName: nameof(Index));
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

            var productTypeFromDbFirst = _unitOfWork.ProductTypes
                .GetFirstOrDefault(u =>u.Id == id);
            if (productTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(productTypeFromDbFirst);
        
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductTypes obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductTypes.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product type has been updated";
                return RedirectToAction(nameof(Index));
         
            }
            return View(obj);
        
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTypeFromDbFirst = _unitOfWork.ProductTypes.GetFirstOrDefault(u =>u.Id== id);
            if (productTypeFromDbFirst == null)
                return NotFound();
            return View(productTypeFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var productTypeFromDbFirst = _unitOfWork.ProductTypes
                .GetFirstOrDefault(u =>u.Id == id);
            if (productTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(productTypeFromDbFirst);
        
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.ProductTypes.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
           
            _unitOfWork.ProductTypes.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Type has been deleted";
            return RedirectToAction(nameof(Index));
           
        }
    }
}
