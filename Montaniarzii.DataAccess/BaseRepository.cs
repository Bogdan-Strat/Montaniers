using Montaniarzii.Common;
using Montaniarzii.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.DataAccess
{
    internal class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly MontaniarziiContext Context;

        public BaseRepository(MontaniarziiContext context)
        {
            Context = context;
        }

        public void HardDelete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return entity;
        }

    }
}
