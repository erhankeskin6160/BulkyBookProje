using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAcces.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; set; }

        ICoverTypeRepository CoverType { get; set; }

        IProductRepository Product { get; set; }

        void Save();
    }
}
