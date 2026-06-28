using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.RespositoryContracts.Common;
using TC.Domain.Entities;

namespace TC.Application.RespositoryContracts
{
    public interface IBoardRepository : IBaseRepository<Board>
    {
        #region Board Getter Repository Methods
        /// <summary>
        /// Gets all boards asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of boards.</returns>
        Task<IEnumerable<Board>> GetAllBoardsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets boards by user ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of boards.</returns>
        Task<IEnumerable<Board>> GetBoardsByUserIdAsync(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a board by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the board.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the board.</returns>
        Task<Board> GetBoardByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion


        #region Board Adder Repository Methods
        /// <summary>
        /// Adds a new board asynchronously.
        /// </summary>
        /// <param name="board">The board to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added board.</returns>
        Task<Board> AddBoardAsync(Board board, CancellationToken cancellationToken = default);
        #endregion


        #region Board Updater Repository Methods
        /// <summary>
        /// Updates an existing board asynchronously.
        /// </summary>
        /// <param name="board">The board to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated board.</returns>
        Task<Board> UpdateBoardAsync(Board board, CancellationToken cancellationToken = default);
        #endregion  


        #region Board Deleter Repository Methods
        /// <summary>
        /// Deletes a board by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the board to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteBoardAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}