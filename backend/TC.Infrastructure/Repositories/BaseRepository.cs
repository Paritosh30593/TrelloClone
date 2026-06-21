using Microsoft.EntityFrameworkCore;
using TC.Domain.Common;
using TC.Domain.RespositoryContracts;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories
{
    public class BaseRepository<T, K>(TrelloCloneDBContext context) : IBaseRepository<T, K> where T : BaseEntity<K>
    {
        protected readonly TrelloCloneDBContext Context = context;

        public void Create(T entity)
        {
            Context.Add(entity);
        }

        public void Update(T entity)
        {
            Context.Update(entity);
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public Task<T?> Get(K id, CancellationToken cancellationToken)
        {
            return Context.Set<T>().FirstOrDefaultAsync(x => x.ID != null && x.ID.Equals(id), cancellationToken);
        }

        public Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return Context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}