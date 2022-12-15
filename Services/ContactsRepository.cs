using AspnetcoreEcommercedemo.Data;
using AspnetcoreEcommercedemo.Interfaces;
using AspnetcoreEcommercedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Services
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddContact(Contacts contacts)
        {
            _context.Contacts.Add(contacts);
        }

        public List<Contacts> GetAllContacts()
        {
            return _context.Contacts.ToList();
        }

        public Contacts GetContact(int id)
        {
            return _context.Contacts.Find(id);
        }

        public void RemoveContact(int id)
        {
            _context.Contacts.Remove(GetContact(id));
        }

        public void UpdateContact(Contacts contacts)
        {
            _context.Contacts.Update(contacts);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

    }
}
