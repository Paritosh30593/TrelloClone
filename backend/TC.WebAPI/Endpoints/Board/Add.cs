using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.BoardDTO;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Board
{
    [Route("api/boards")]
    public class Add(IBoardService boardService)
    : EndpointBaseAsync
        .WithRequest<BoardAddRequest>
        .WithActionResult<BoardResponse>
    {
        private readonly IBoardService _boardService = boardService;

        [HttpPost]
        public override async Task<ActionResult<BoardResponse>> HandleAsync(BoardAddRequest boardRequest, CancellationToken cancellationToken = default)
        {
            BoardResponse board = await _boardService.AddBoardAsync(boardRequest, cancellationToken);

            return board == null
                ? BadRequest()
                : Ok(board);
        }
    }
}