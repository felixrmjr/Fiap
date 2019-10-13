using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Fiap.Domain.Entities.Base;

namespace Fiap.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity, TIdentity> where TEntity : EntityBase<TIdentity> 
    {
    }
}
