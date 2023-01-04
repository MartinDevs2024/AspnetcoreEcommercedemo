using AspnetcoreEcommercedemo.Interfaces;
using AspnetcoreEcommercedemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _repo;
        private readonly IFileManager _fileManager;

        public ContactsController(IContactsRepository repo,
                     IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var contact = _repo.GetAllContacts();
            return View(contact);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new ContactsViewModel());
            }
            else 
            {
                var contact = _repo.GetContact((int)id);
                return View(new ContactsViewModel
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                    CurrentImage = contact.Photo
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactsViewModel vm)
        {
            var contact = new Models.Contacts
            {
                Id = vm.Id,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber
            };
            if (vm.Photo == null)
            {
                contact.Photo = vm.CurrentImage;
            }
            else
            {
                contact.Photo = await _fileManager.SaveImage(vm.Photo);
            }
            if (contact.Id > 0)
                _repo.UpdateContact(contact);
            else
                _repo.AddContact(contact);
            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(contact);
        }

        [HttpGet("/ContactPhoto/{contactPhoto}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult ContactPhoto(string contactPhoto)
        {
            var mine = contactPhoto.Substring(contactPhoto.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(contactPhoto), $"contactPhoto/{mine}");
        
        }
    }
}
