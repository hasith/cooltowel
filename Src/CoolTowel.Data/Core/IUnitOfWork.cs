using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        IRepository<T> GetEntityRepository<T>() where T : class, IIdentifier;
        R GetCustomRepository<R>() where R : class;
        void RegisterEntityRepository<E>(IRepository<E> repository) where E : class, IIdentifier;
        void RegisterCustomRepository<R>(R repository) where R : class;
    }
}
