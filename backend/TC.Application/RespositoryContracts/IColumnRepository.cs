using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.RespositoryContracts.Common;
using TC.Domain.Entities;

namespace TC.Application.RespositoryContracts
{
    public interface IColumnRepository : IBaseRepository<Column>
    {
        #region Getters
        /// <summary>
        /// Gets all columns asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of columns.</returns>
        Task<IEnumerable<Column>> GetAllColumnsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all columns by board ID asynchronously.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of columns.</returns>
        Task<IEnumerable<Column>> GetAllColumnsByBoardIdAsync(int boardId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a column by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the column.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the column.</returns>
        Task<Column> GetColumnByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion


        #region Adders
        /// <summary>
        /// Adds a new column asynchronously.
        /// </summary>
        /// <param name="column">The column to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added column.</returns>
        Task<Column> AddColumnAsync(Column column, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a list of default columns asynchronously.
        /// </summary>
        /// <param name="columns">The list of columns to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> AddDefaultColumnsAsync(List<Column> columns, CancellationToken cancellationToken = default);
        #endregion


        #region Updaters
        /// <summary>
        /// Updates an existing column asynchronously.
        /// </summary>
        /// <param name="column">The column to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated column.</returns>
        Task<Column> UpdateColumnAsync(Column column, CancellationToken cancellationToken = default);
        #endregion


        #region Deleters
        /// <summary>
        /// Deletes a column by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the column to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteColumnAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}