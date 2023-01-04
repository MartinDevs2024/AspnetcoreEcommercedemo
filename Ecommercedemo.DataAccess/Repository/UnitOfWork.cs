using AspnetcoreEcommercedemo.DataAccess.Data;
using AspnetcoreEcommercedemo.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ProductTypes = new ProductTypesRepository(_db);
        }
        public IProductTypesRepository ProductTypes { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
