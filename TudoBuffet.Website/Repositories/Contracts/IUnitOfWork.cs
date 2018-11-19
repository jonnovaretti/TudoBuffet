using System;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IUnitOfWork<T> : IDisposable
    {
        T BeginTransaction();
        void Rollback();
        void Commit();
    }
}
