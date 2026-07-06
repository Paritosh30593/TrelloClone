using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.BoardDTO;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Board
{
    [Route("api/boards")]
    public class Update(IBoardService boardService)
    : EndpointBaseAsync
        .WithRequest<BoardUpdateRequest>
        .WithActionResult<BoardResponse>
    {
        private readonly IBoardService _boardService = boardService;

        [HttpPut]
        public override async Task<ActionResult<BoardResponse>> HandleAsync(BoardUpdateRequest boardRequest, CancellationToken cancellationToken = default)
        {
            BoardResponse board = await _boardService.UpdateBoardAsync(boardRequest, cancellationToken);

            return board == null
                ? NotFound()
                : Ok(board);
        }
    }
}