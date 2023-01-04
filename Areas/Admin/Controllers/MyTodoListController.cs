using AspnetcoreEcommercedemo.DataAccess.Data;
using AspnetcoreEcommercedemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MyTodoListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyTodoListController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var todoList = await _context.TodoLists.ToListAsync(); 
            return View(todoList);
        }

        //Get /todo/create
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TodoList item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();

                TempData["save"] = "The item has been added!";
                return RedirectToAction("Index");
            }
            return View(item);
        
        }

        //Get /todo/item/5
        public async Task<IActionResult> Edit(int id)
        {
            TodoList item = await _context.TodoLists.FindAsync(id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        //Post /todo/item/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoList item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();

                TempData["edit"] = "The item has been updated!";

                return RedirectToAction("Index");
            }
            return View(item);
        }

        //Get /todo/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            TodoList item = await _context.TodoLists.FindAsync(id);
            if (item == null)
            {
                TempData["error"] = "The item does not exist!";
            }
            else 
            {
                _context.TodoLists.Remove(item);
                await _context.SaveChangesAsync();

                TempData["delete"] = "The item has been deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
