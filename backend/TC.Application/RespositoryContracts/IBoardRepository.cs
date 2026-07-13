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
        /// <remarks> This method retrieves all boards from the data source. It can be used to fetch a complete list of boards without any filtering or pagination. </remarks>
        Task<IEnumerable<Board>> GetAllBoardsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets boards by user ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of boards.</returns>
        /// <remarks> This method retrieves all boards associated with a specific user. It can be used to fetch boards that belong to a particular user, allowing for user-specific board management. </remarks>
        Task<IEnumerable<Board>> GetBoardsByUserIdAsync(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a board by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the board.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the board.</returns>
        /// <remarks> This method retrieves a specific board based on its unique identifier. It can be used to fetch detailed information about a particular board, which is useful for viewing or editing board details. </remarks>
        Task<Board> GetBoardByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion


        #region Board Adder Repository Methods
        /// <summary>
        /// Adds a new board asynchronously.
        /// </summary>
        /// <param name="board">The board to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added board.</returns>
        /// <remarks> This method allows for the creation of a new board in the data source. It can be used to add boards with specific properties, enabling users to create new boards for their projects or tasks. </remarks>
        Task<Board> AddBoardAsync(Board board, CancellationToken cancellationToken = default);
        #endregion


        #region Board Updater Repository Methods
        /// <summary>
        /// Updates an existing board asynchronously.
        /// </summary>
        /// <param name="board">The board to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated board.</returns>
        /// <remarks> This method allows for the modification of an existing board's properties. It can be used to update board details such as name, description, or other relevant information, facilitating the management of boards over time. </remarks>
        Task<Board> UpdateBoardAsync(Board board, CancellationToken cancellationToken = default);
        #endregion  


        #region Board Deleter Repository Methods
        /// <summary>
        /// Deletes a board by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the board to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
        /// <remarks> This method allows for the removal of a board from the data source based on its unique identifier. It can be used to delete boards that are no longer needed, helping to maintain a clean and organized set of boards. </remarks>
        Task<bool> DeleteBoardAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}