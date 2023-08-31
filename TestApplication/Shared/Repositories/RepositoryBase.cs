using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using TestApplication.Model.Base;
using TestApplication.Model.Database;

namespace TestApplication.Shared.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class,IEntity
    {
        protected RepositoryBase(UserContext dbDbContext)
        {

            DbContext = dbDbContext;
        }

        protected virtual UserContext DbContext { get; }


        protected abstract Expression<Func<T, int>> Key { get; }

        protected abstract DbSet<T> DbSet { get; }

        protected abstract string DbSetDisplayName { get; }

        protected abstract Expression<Func<T, object>> DefaultOrderBy { get; }

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => await DbContext.Database.BeginTransactionAsync(cancellationToken);

     
    }
}