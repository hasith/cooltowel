using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Core
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> GetAll(string[] includes = null);

        T GetById(int id);

        void InsertOrUpdate(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}

