using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProductTypesRepository ProductTypes { get; }

        void Save();
    }
}
