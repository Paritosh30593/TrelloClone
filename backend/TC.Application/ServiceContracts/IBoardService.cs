using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.DTO.BoardDTO;

namespace TC.Application.ServiceContracts
{
    public interface IBoardService
    {
        #region Getters
        /// <summary>
        /// Gets all boards for a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A collection of <see cref="BoardResponse"/> objects.</returns>
        /// <exception cref="System.NotImplementedException">Thrown when the method is not implemented.</exception>
        Task<List<BoardResponse>> GetAllBoardsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all boards for a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A collection of <see cref="BoardResponse"/> objects.</returns>
        /// <exception cref="System.NotImplementedException">Thrown when the method is not implemented.</exception>
        Task<List<BoardResponse>> GetAllBoardsByUserIdAsync(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a board by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the board.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A <see cref="BoardResponse"/> object if found; otherwise, null.</returns>
        /// <exception cref="System.NotImplementedException">Thrown when the method is not implemented.</exception>
        Task<BoardResponse> GetBoardByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion


        #region Adders
        /// <summary>
        /// Adds a new board asynchronously.
        /// </summary>
        /// <param name="request">The request containing the board details.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added board details.</returns>
        Task<BoardResponse> AddBoardAsync(BoardAddRequest request, CancellationToken cancellationToken = default);
        #endregion


        #region Updaters
        /// <summary>
        /// Updates an existing board asynchronously.
        /// </summary>
        /// <param name="request">The request containing the updated board details.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated board details.</returns>
        Task<BoardResponse> UpdateBoardAsync(BoardUpdateRequest request, CancellationToken cancellationToken = default);
        #endregion


        #region Deleters
        /// <summary>
        /// Deletes a board asynchronously.
        /// </summary>
        /// <param name="id">The ID of the board to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteBoardAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}