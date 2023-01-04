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
    public class SpecialTagRepository : Repository<SpecialTag>, ISpecialTagRepository
    {
        private readonly ApplicationDbContext _db;

        public SpecialTagRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(SpecialTag obj)
        {
            _db.SpecialTags.Update(obj);
        }
    }
}
