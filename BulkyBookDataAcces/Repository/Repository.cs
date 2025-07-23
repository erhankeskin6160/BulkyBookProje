using BulkyBook.DataAcces.Data;
using BulkyBook.DataAcces.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAcces.Repository
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        public DbSet<T> DbSet { get; set; }

        public Repository(ApplicationDbContext db )
        {
            _db = db;
            this.DbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string? includeProperties=null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
           IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
             DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            DbSet.RemoveRange(entity);
        }
    }
}
