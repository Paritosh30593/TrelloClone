using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TC.Application.RespositoryContracts;
using TC.Domain.Entities;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories
{
    public class ColumnRepository : BaseRepository<Column>, IColumnRepository
    {
        private readonly TrelloCloneDBContext _context;

        public ColumnRepository(TrelloCloneDBContext context) : base(context)
        {
            _context = context;
        }

        #region Getters
        public async Task<IEnumerable<Column>> GetAllColumnsAsync(CancellationToken cancellationToken = default)
        {
            return await ListAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Column>> GetAllColumnsByBoardIdAsync(int boardId, CancellationToken cancellationToken = default)
        {
            return await _context.Column
                .Where(c => c.BoardId == boardId)
                .OrderByDescending(c => c.SortOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task<Column> GetColumnByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(id, cancellationToken);
        }
        #endregion


        #region Adders
        public async Task<Column> AddColumnAsync(Column column, CancellationToken cancellationToken = default)
        {
            int id = await CreateAsync(column, cancellationToken);

            return id > 0 ? await GetByIdAsync(id, cancellationToken) : null;
        }
        #endregion


        #region Updaters
        public async Task<Column> UpdateColumnAsync(Column column, CancellationToken cancellationToken = default)
        {
            bool IsSuccess = await UpdateAsync(column, cancellationToken);

            return IsSuccess ? await GetByIdAsync(column.Id, cancellationToken) : null;
        }
        #endregion


        #region Deleters
        public async Task<bool> DeleteColumnAsync(int id, CancellationToken cancellationToken = default)
        {
            Column columnToDelete = await GetByIdAsync(id, cancellationToken);

            return columnToDelete != null && await DeleteAsync(columnToDelete, cancellationToken);
        }
        #endregion
    }
}