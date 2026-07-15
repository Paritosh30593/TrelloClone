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

        public async Task<IEnumerable<Column>> GetColumnsByBoardIdAsync(int boardId, CancellationToken cancellationToken = default)
        {
            return await _context.Column
                .Where(c => c.BoardId == boardId)
                .OrderBy(c => c.SortOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task<Column> GetColumnByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Column>> GetBoardColumnsWithCardsAsync(int boardId, CancellationToken cancellationToken = default)
        {
            return await _context.Column
                .Include(c => c.Cards.OrderBy(card => card.SortOrder))
                .Where(c => c.BoardId == boardId)
                .OrderBy(c => c.SortOrder)
                .ToListAsync(cancellationToken);
        }
        #endregion


        #region Adders
        public async Task<Column> AddColumnAsync(Column column, CancellationToken cancellationToken = default)
        {

            return await CreateAsync(column, cancellationToken);
        }

        public async Task<int> AddDefaultColumnsAsync(List<Column> columns, CancellationToken cancellationToken = default)
        {
            await _context.Column.AddRangeAsync(columns, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken); ;
        }
        #endregion


        #region Updaters
        public async Task<Column> UpdateColumnAsync(Column column, CancellationToken cancellationToken = default)
        {
            return await UpdateAsync(column, cancellationToken);
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