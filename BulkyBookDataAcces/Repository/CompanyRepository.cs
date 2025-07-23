using BulkyBook.DataAcces.Data;
using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAcces.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {

        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
           _db.Companies.Update(company);
        }
    }


}
