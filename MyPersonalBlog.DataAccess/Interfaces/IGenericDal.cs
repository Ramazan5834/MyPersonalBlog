using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Interfaces;

namespace MyPersonalBlog.DataAccess.Interfaces
{
    public interface IGenericDal<TEntity> where TEntity : class,ITable,new()
    {
        TEntity AddWithRetObject(TEntity entity);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);

        TEntity GetById(int id);
        List<TEntity> GetAll();
    }
}
