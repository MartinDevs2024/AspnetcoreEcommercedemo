using AspnetcoreEcommercedemo.DataAccess.Data;
using AspnetcoreEcommercedemo.DataAccess.Repository.IRepository;
using AspnetcoreEcommercedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.DataAccess.Repository
{
    public class ProductTypesRepository : Repository<ProductTypes>, IProductTypesRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductTypesRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(ProductTypes obj)
        {
            _db.ProductTypes.Update(obj);
        }
    }
}
