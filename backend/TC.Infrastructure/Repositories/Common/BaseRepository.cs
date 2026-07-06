
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TC.Application.RespositoryContracts.Common;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TrelloCloneDBContext _context;

        private readonly IDapperUnitOfWork _uow;
        public DbConnection _db => _uow.DBConnection;
        public DbTransaction _tx => _uow.DBTransaction;

        public BaseRepository(TrelloCloneDBContext context, IDapperUnitOfWork uow = null)
        {
            _context = context;
            _uow = uow;
        }

        #region IBaseRepository Implementation
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            T addedEntity = (await _context.AddAsync(entity, cancellationToken)).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            return addedEntity;
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _context.RemoveRange(entities);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        #endregion


        #region IReadOnlyRepository Implementation
        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            T entity = await _context
                .Set<T>()
                .FindAsync([id], cancellationToken);

            return entity ?? null;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<T>()
                .CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> ListRangeAsync(IEnumerable<object> ids, CancellationToken cancellationToken = default)
        {
            List<object> idList = [.. ids];

            string tableName = _context.Model.FindEntityType(typeof(T))?.GetTableName();

            if (string.IsNullOrWhiteSpace(tableName))
            {
                return [];
            }

            return await _context
                .Set<T>()
                .FromSql($"SELECT * FROM {tableName} WHERE Id IN ({string.Join(",", idList)})")
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<T>()
                .ToListAsync(cancellationToken);
        }
        #endregion
    }
}