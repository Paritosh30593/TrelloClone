using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.DTO.BoardDTO;
using TC.Application.RespositoryContracts;
using TC.Application.ServiceContracts;
using TC.Domain.Entities;

namespace TC.Application.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IColumnService _columnService;

        public BoardService(IBoardRepository boardRepository, IColumnService columnService)
        {
            _boardRepository = boardRepository;
            _columnService = columnService;
        }

        #region Getters
        public async Task<List<BoardResponse>> GetAllBoardsAsync(CancellationToken cancellationToken = default)
        {
            return (await _boardRepository.GetAllBoardsAsync(cancellationToken))
                .Select(s => s.ToBoardResponse())
                .ToList();
        }

        public async Task<List<BoardResponse>> GetBoardsByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            if (userId == null)
            {
                return new List<BoardResponse>();
            }
            return (await _boardRepository.GetBoardsByUserIdAsync(userId, cancellationToken))
                .Select(s => s.ToBoardResponse())
                .ToList();
        }

        public async Task<BoardResponse> GetBoardByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                return null;
            }
            return (await _boardRepository.GetBoardByIdAsync(id, cancellationToken))?.ToBoardResponse();
        }
        #endregion


        #region Adders
        public async Task<BoardResponse> AddBoardAsync(BoardAddRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "BoardAddRequest cannot be null.");
            }

            Board board = request.ToBoard();

            board = await _boardRepository.AddBoardAsync(board, cancellationToken)
                        ?? throw new InvalidOperationException("Failed to add the board.");

            _ = await _columnService.AddDefaultColumnsByBoardIdAsync(board.Id, cancellationToken);

            return board.ToBoardResponse();
        }
        #endregion


        #region Updaters
        public async Task<BoardResponse> UpdateBoardAsync(BoardUpdateRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "BoardUpdateRequest cannot be null.");
            }

            Board board = request.ToBoard();

            board = await _boardRepository.UpdateBoardAsync(board, cancellationToken)
                        ?? throw new InvalidOperationException("Failed to update the board.");

            return board.ToBoardResponse();
        }
        #endregion


        #region Deleters
        public async Task<bool> DeleteBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Board ID must be greater than zero.");
            }

            return await _boardRepository.DeleteBoardAsync(id, cancellationToken);
        }
        #endregion
    }
}