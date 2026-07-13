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
        /// <remarks> This method retrieves all columns from the data source. It can be used to fetch a complete list of columns without any filtering or pagination. </remarks>
        Task<IEnumerable<Column>> GetAllColumnsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all columns by board ID asynchronously.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of columns.</returns>
        /// <remarks> This method retrieves all columns associated with a specific board. It can be used to fetch columns that belong to a particular board, allowing for board-specific column management. </remarks>
        Task<IEnumerable<Column>> GetColumnsByBoardIdAsync(int boardId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a column by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the column.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the column.</returns>
        /// <remarks> This method retrieves a specific column based on its unique identifier. It can be used to fetch detailed information about a particular column, which is useful for viewing or editing column details. </remarks>
        Task<Column> GetColumnByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all columns with their associated cards for a specific board asynchronously.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of columns with their associated cards.</returns>
        /// <remarks> This method retrieves all columns along with their associated cards for a specific board. It can be used to fetch a complete view of a board's structure, including the columns and the cards within them, facilitating board management and visualization. </remarks>
        Task<IEnumerable<Column>> GetBoardColumnsWithCardsAsync(int boardId, CancellationToken cancellationToken = default);
        #endregion


        #region Adders
        /// <summary>
        /// Adds a new column asynchronously.
        /// </summary>
        /// <param name="column">The column to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added column.</returns>
        /// <remarks> This method allows for the creation of a new column in the data source. It can be used to add columns with specific properties, enabling users to create new columns for their boards or projects. </remarks>
        Task<Column> AddColumnAsync(Column column, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a list of default columns asynchronously.
        /// </summary>
        /// <param name="columns">The list of columns to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
        /// <remarks> This method allows for the addition of a predefined set of columns to the data source. It can be used to initialize a board with default columns, facilitating the setup of new boards with a standard structure. </remarks>
        Task<int> AddDefaultColumnsAsync(List<Column> columns, CancellationToken cancellationToken = default);
        #endregion


        #region Updaters
        /// <summary>
        /// Updates an existing column asynchronously.
        /// </summary>
        /// <param name="column">The column to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated column.</returns>
        /// <remarks> This method allows for the modification of an existing column's properties. It can be used to update column details such as name, description, or other relevant information, facilitating the management of columns over time. </remarks>
        Task<Column> UpdateColumnAsync(Column column, CancellationToken cancellationToken = default);
        #endregion


        #region Deleters
        /// <summary>
        /// Deletes a column by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the column to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        /// <remarks> This method allows for the removal of a column from the data source based on its unique identifier. It can be used to delete columns that are no longer needed, helping to maintain a clean and organized set of columns. </remarks>
        Task<bool> DeleteColumnAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}