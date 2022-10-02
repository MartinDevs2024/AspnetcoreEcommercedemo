using AspnetcoreEcommercedemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.UI.Controllers
{
    [Area("UI")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ApplicationDbContext _context;

        public ProductController(ILogger<ProductController> Logger, ApplicationDbContext context)
        {
            _logger = Logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList());
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        
        
        
        }

    }
}
