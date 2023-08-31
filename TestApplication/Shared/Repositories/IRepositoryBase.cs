using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft;
using Microsoft.EntityFrameworkCore.Storage;
using TestApplication.Model.Base;

namespace TestApplication.Shared.Repositories
{
    public interface IRepositoryBase<T> where T : IEntity
    {

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        
    }
}