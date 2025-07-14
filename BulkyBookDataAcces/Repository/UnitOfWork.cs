﻿using BulkyBook.DataAcces.Data;
using BulkyBook.DataAcces.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAcces.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
        }

        public ICategoryRepository Category { get; set; }
        public ICoverTypeRepository CoverType { get; set; }

        public IProductRepository Product { get; set; }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
