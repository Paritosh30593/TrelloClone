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
        /// <remarks> This method retrieves all boards associated with a specific user. It can be used to fetch boards that belong to a particular user, allowing for user-specific board management. </remarks>
        Task<List<BoardResponse>> GetAllBoardsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all boards for a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A collection of <see cref="BoardResponse"/> objects.</returns>
        /// <remarks> This method retrieves all boards associated with a specific user. It can be used to fetch boards that belong to a particular user, allowing for user-specific board management. </remarks>
        Task<List<BoardResponse>> GetBoardsByUserIdAsync(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a board by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the board.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A <see cref="BoardResponse"/> object if found; otherwise, null.</returns>
        /// <remarks> This method retrieves a specific board based on its unique identifier. It can be used to fetch detailed information about a particular board, which is useful for viewing or editing board details. </remarks>
        Task<BoardResponse> GetBoardByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion


        #region Adders
        /// <summary>
        /// Adds a new board asynchronously.
        /// </summary>
        /// <param name="request">The request containing the board details.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added board details.</returns>
        /// <remarks> This method allows for the creation of a new board in the data source. It can be used to add boards with specific properties, enabling users to create new boards for their projects or tasks. </remarks>
        Task<BoardResponse> AddBoardAsync(BoardAddRequest request, CancellationToken cancellationToken = default);
        #endregion


        #region Updaters
        /// <summary>
        /// Updates an existing board asynchronously.
        /// </summary>
        /// <param name="request">The request containing the updated board details.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated board details.</returns>
        /// <remarks> This method allows for the modification of an existing board's properties. It can be used to update board details such as name, description, or other relevant information, facilitating the management of boards over time. </remarks>
        Task<BoardResponse> UpdateBoardAsync(BoardUpdateRequest request, CancellationToken cancellationToken = default);
        #endregion


        #region Deleters
        /// <summary>
        /// Deletes a board asynchronously.
        /// </summary>
        /// <param name="id">The ID of the board to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        /// <remarks> This method allows for the removal of a board from the data source based on its unique identifier. It can be used to delete boards that are no longer needed, helping to maintain a clean and organized set of boards. </remarks>
        Task<bool> DeleteBoardAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}