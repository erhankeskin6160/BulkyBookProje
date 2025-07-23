using BulkyBook.Models;

namespace BulkyBook.DataAcces.Repository.IRepository
{
    public interface ICompanyRepository: IRepository<Company>
    {
        void Update(Company company);
    }
}
