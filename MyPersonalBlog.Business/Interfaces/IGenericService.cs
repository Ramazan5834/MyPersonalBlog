using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Interfaces;

namespace MyPersonalBlog.Business.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : class, ITable, new()
    {
        TEntity AddWithRetObject(TEntity entity);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);

        TEntity GetById(int id);
        List<TEntity> GetAll();
    }
}
