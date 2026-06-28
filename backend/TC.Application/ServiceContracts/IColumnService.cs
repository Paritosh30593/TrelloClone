using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.DTO.ColumnDTO;

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
        Task<List<ColumnResponse>> GetAllColumnsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all columns for a specific board asynchronously.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of <see cref="ColumnResponse"/> objects.</returns>
        Task<List<ColumnResponse>> GetAllColumnsByBoardIdAsync(int boardId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a column by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the column.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="ColumnResponse"/> object.</returns>
        Task<ColumnResponse> GetColumnByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion


        #region Adders
        /// <summary>
        /// Adds a new column asynchronously.
        /// </summary>
        /// <param name="columnRequest">The column request object.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="ColumnResponse"/> object.</returns>
        Task<ColumnResponse> AddColumnAsync(ColumnAddRequest columnRequest, CancellationToken cancellationToken = default);
        #endregion


        #region Updaters
        /// <summary>
        /// Updates an existing column asynchronously.
        /// </summary>
        /// <param name="columnRequest">The column request object.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="ColumnResponse"/> object.</returns>
        Task<ColumnResponse> UpdateColumnAsync(ColumnUpdateRequest columnRequest, CancellationToken cancellationToken = default);
        #endregion


        #region Deleters
        /// <summary>
        /// Deletes a column by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the column.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteColumnAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}