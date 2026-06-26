using Microsoft.EntityFrameworkCore;
using TC.Domain.Common;
using TC.Domain.RespositoryContracts;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories
{
    public class BaseRepository<T, K> : IBaseRepository<T, K> where T : BaseEntity<K>
    {
        protected readonly TrelloCloneDBContext _context;

        public BaseRepository(TrelloCloneDBContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public Task<T?> GetByIdAsync(K id, CancellationToken cancellationToken)
        {
            return _context.Set<T>().FirstOrDefaultAsync(x => x.Id != null && x.Id.Equals(id), cancellationToken);
        }

        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}