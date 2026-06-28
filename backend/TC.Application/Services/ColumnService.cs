using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.DTO.ColumnDTO;
using TC.Application.RespositoryContracts;
using TC.Application.ServiceContracts;
using TC.Domain.Entities;

namespace TC.Application.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _columnRepository;

        public ColumnService(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository;
        }

        #region Getters
        public async Task<List<ColumnResponse>> GetAllColumnsAsync(CancellationToken cancellationToken = default)
        {
            return (await _columnRepository.GetAllColumnsAsync(cancellationToken))
                .Select(c => c.ToColumnResponse())
                .ToList();
        }

        public async Task<List<ColumnResponse>> GetAllColumnsByBoardIdAsync(int boardId, CancellationToken cancellationToken = default)
        {
            if (boardId <= 0)
            {
                return new List<ColumnResponse>();
            }
            return (await _columnRepository.GetAllColumnsByBoardIdAsync(boardId, cancellationToken))
                .Select(c => c.ToColumnResponse())
                .ToList();
        }

        public async Task<ColumnResponse> GetColumnByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                return null;
            }
            return (await _columnRepository.GetColumnByIdAsync(id, cancellationToken))?.ToColumnResponse();
        }
        #endregion


        #region Adders
        public async Task<ColumnResponse> AddColumnAsync(ColumnAddRequest columnRequest, CancellationToken cancellationToken = default)
        {
            if (columnRequest == null)
            {
                throw new ArgumentNullException(nameof(columnRequest), "ColumnAddRequest cannot be null.");
            }

            Column column = columnRequest.ToColumn();

            column = await _columnRepository.AddColumnAsync(column, cancellationToken)
                ?? throw new InvalidOperationException("Column cannot be null.");

            return column.ToColumnResponse();
        }
        #endregion


        #region Updaters
        public async Task<ColumnResponse> UpdateColumnAsync(ColumnUpdateRequest columnRequest, CancellationToken cancellationToken = default)
        {
            if (columnRequest == null)
            {
                throw new ArgumentNullException(nameof(columnRequest), "ColumnUpdateRequest cannot be null.");
            }

            Column column = columnRequest.ToColumn();

            column = await _columnRepository.UpdateColumnAsync(column, cancellationToken)
                ?? throw new InvalidOperationException("Column cannot be null.");

            return column.ToColumnResponse();
        }
        #endregion


        #region Deleters
        public async Task<bool> DeleteColumnAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Column ID must be greater than zero.");
            }

            return await _columnRepository.DeleteColumnAsync(id, cancellationToken);
        }
        #endregion
    }
}