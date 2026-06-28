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
    public class BoardRepository : BaseRepository<Board>, IBoardRepository
    {
        private readonly TrelloCloneDBContext _context;

        public BoardRepository(TrelloCloneDBContext context) : base(context)
        {
            _context = context;
        }

        #region Getters
        public async Task<IEnumerable<Board>> GetAllBoardsAsync(CancellationToken cancellationToken = default)
        {
            return await ListAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Board>> GetBoardsByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Board
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Board> GetBoardByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(id, cancellationToken);
        }
        #endregion


        #region Adders
        public async Task<Board> AddBoardAsync(Board board, CancellationToken cancellationToken = default)
        {
            int id = await CreateAsync(board, cancellationToken);

            return id > 0 ? await GetByIdAsync(id, cancellationToken) : null;
        }
        #endregion


        #region Updaters
        public async Task<Board> UpdateBoardAsync(Board board, CancellationToken cancellationToken = default)
        {
            bool IsSuccess = await UpdateAsync(board, cancellationToken);

            return IsSuccess ? await GetByIdAsync(board.Id, cancellationToken) : null;
        }
        #endregion


        #region Deleters
        public async Task<bool> DeleteBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            Board boardToDelete = await GetByIdAsync(id, cancellationToken);

            return boardToDelete != null && await DeleteAsync(boardToDelete, cancellationToken);
        }
        #endregion
    }
}