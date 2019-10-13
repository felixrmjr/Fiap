using Fiap.Domain.Entities.Base;
using Fiap.Domain.Interfaces;

namespace Fiap.Domain.Repository
{
    public abstract class RepositoryBase<TEntity, TIdentity> : IRepositoryBase<TEntity, TIdentity> where TEntity : EntityBase<TIdentity>
    {
        protected readonly string ConnectionString;

        protected RepositoryBase()
        {
            ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\felix\\Desktop\\Fiap\\Db\\FiapDb.mdf;Integrated Security=True;Connect Timeout=30";
        }
    }
}
