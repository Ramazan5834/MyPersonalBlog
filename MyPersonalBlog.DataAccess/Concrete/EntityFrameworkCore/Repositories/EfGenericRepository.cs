using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Interfaces;

namespace MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericRepository<TEntity> : IGenericDal<TEntity> where TEntity : class, ITable, new()
    {
        private readonly MyPersonalBlogContext _context;
        public EfGenericRepository(MyPersonalBlogContext context)
        {
            _context = context;
        }

        public TEntity AddWithRetObject(TEntity entity)
        {
            var addEntity = _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return addEntity.Entity;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();

        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

    }
}
