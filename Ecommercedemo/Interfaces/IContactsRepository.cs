using AspnetcoreEcommercedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Interfaces
{
    public interface IContactsRepository
    {
        Contacts GetContact(int id);
        List<Contacts> GetAllContacts();
        void AddContact(Contacts contacts);
        void UpdateContact(Contacts contacts);
        void RemoveContact(int id);
        Task<bool> SaveChangesAsync();
    }
}
