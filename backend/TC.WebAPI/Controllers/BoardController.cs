using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.BoardDTO;
using TC.Application.ServiceContracts;
using TC.WebAPI.Controllers.Base;

namespace TC.WebAPI.Controllers
{
    public class BoardController : BaseController
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        #region Getters
        public async Task<ActionResult<List<BoardResponse>>> GetAll()
        {
            List<BoardResponse> boards = await _boardService.GetAllBoardsAsync();

            return boards == null
                ? NotFound()
                : Ok(boards);
        }

        public async Task<ActionResult<List<BoardResponse>>> GetAllByUserId(string userId)
        {
            List<BoardResponse> boards = await _boardService.GetAllBoardsByUserIdAsync(userId);

            return boards == null
                ? NotFound()
                : Ok(boards);
        }

        public async Task<ActionResult<BoardResponse>> GetBoardById(int id)
        {
            BoardResponse board = await _boardService.GetBoardByIdAsync(id);

            return board == null
               ? NotFound()
               : Ok(board);
        }
        #endregion


        #region Adders
        public async Task<ActionResult<BoardResponse>> AddBoard(BoardAddRequest boardRequest)
        {
            BoardResponse board = await _boardService.AddBoardAsync(boardRequest);

            return board == null
                ? BadRequest()
                : Ok(board);
        }
        #endregion


        #region Updaters
        public async Task<ActionResult<BoardResponse>> UpdateBoard(BoardUpdateRequest boardRequest)
        {
            BoardResponse updatedBoard = await _boardService.UpdateBoardAsync(boardRequest);

            return updatedBoard == null
                ? NotFound()
                : Ok(updatedBoard);
        }
        #endregion


        #region Deleters
        public async Task<ActionResult> DeleteBoard(int id)
        {
            bool isDeleted = await _boardService.DeleteBoardAsync(id);

            return isDeleted
                ? Ok()
                : NotFound();
        }
        #endregion
    }
}