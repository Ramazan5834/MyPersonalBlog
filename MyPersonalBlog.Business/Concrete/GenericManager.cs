using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Interfaces;

namespace MyPersonalBlog.Business.Concrete
{
    public class GenericManager<TEntity> : IGenericService<TEntity> where TEntity : class, ITable, new()
    {
        private readonly IGenericDal<TEntity> _genericDal;
        public GenericManager(IGenericDal<TEntity> genericDal)
        {
            _genericDal = genericDal;
        }

        public TEntity AddWithRetObject(TEntity entity)
        {
            return _genericDal.AddWithRetObject(entity);
        }

        public void Add(TEntity entity)
        {
            _genericDal.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _genericDal.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _genericDal.Update(entity);
        }

        public TEntity GetById(int id)
        {
            return _genericDal.GetById(id);
        }

        public List<TEntity> GetAll()
        {
            return _genericDal.GetAll();
        }
    }
}
