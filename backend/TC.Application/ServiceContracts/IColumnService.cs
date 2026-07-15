using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.DTO.ColumnDTO;
using TC.Application.DTO.ColumnWithCardsDTO;

namespace TC.Application.ServiceContracts
{
    public interface IColumnService
    {
        #region Getters
        /// <summary>
        /// Gets all columns asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of <see cref="ColumnResponse"/> objects.</returns>
        /// <remarks> This method is intended to be used when retrieving all columns in the system. </remarks>
        Task<List<ColumnResponse>> GetAllColumnsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all columns for a specific board asynchronously.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of <see cref="ColumnResponse"/> objects.</returns>
        /// <remarks> This method is intended to be used when retrieving all columns associated with a specific board. </remarks>
        Task<List<ColumnResponse>> GetColumnsByBoardIdAsync(int boardId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a column by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the column.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="ColumnResponse"/> object.</returns>
        /// <remarks> This method is intended to be used when retrieving a specific column by its unique identifier. </remarks>
        Task<ColumnResponse> GetColumnByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all columns with their associated cards for a specific board asynchronously.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of <see cref="ColumnWithCardsResponse"/> objects.</returns>
        /// <remarks> This method is intended to be used when retrieving all columns along with their associated cards for a specific board. </remarks>
        Task<List<ColumnWithCardsResponse>> GetBoardColumnsWithCardsAsync(int boardId, CancellationToken cancellationToken = default);
        #endregion


        #region Adders
        /// <summary>
        /// Adds a new column asynchronously.
        /// </summary>
        /// <param name="columnRequest">The column request object.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="ColumnResponse"/> object.</returns>
        /// <remarks> This method is intended to be used when a new column is created, and it will add the column to the specified board. </remarks>
        Task<ColumnResponse> AddColumnAsync(ColumnAddRequest columnRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds default columns for a specific board asynchronously.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The number of default columns added.</returns>
        /// <remarks> This method is intended to be used when a new board is created, and it will add default columns to that board. </remarks>
        Task<int> AddDefaultColumnsByBoardIdAsync(int boardId, CancellationToken cancellationToken = default);
        #endregion


        #region Updaters
        /// <summary>
        /// Updates an existing column asynchronously.
        /// </summary>
        /// <param name="columnRequest">The column request object.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="ColumnResponse"/> object.</returns>
        /// <remarks> This method is intended to be used when an existing column needs to be updated with new information. </remarks>
        Task<ColumnResponse> UpdateColumnAsync(ColumnUpdateRequest columnRequest, CancellationToken cancellationToken = default);
        #endregion


        #region Deleters
        /// <summary>
        /// Deletes a column by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the column.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <remarks> This method is intended to be used when an existing column needs to be removed from the system. </remarks>
        Task<bool> DeleteColumnAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}