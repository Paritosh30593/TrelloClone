using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.BoardDTO;
using TC.Application.ServiceContracts;
using Ardalis.ApiEndpoints;
using System.Threading.Tasks;
using System.Threading;

namespace TC.WebAPI.Endpoints.Board
{
    [Route("api/boards")]
    public class GetById(IBoardService boardService)
    : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<BoardResponse>
    {
        private readonly IBoardService _boardService = boardService;

        [HttpGet("{id}")]
        public override async Task<ActionResult<BoardResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            BoardResponse board = await _boardService.GetBoardByIdAsync(id, cancellationToken);

            return board == null
               ? NotFound("Board not found.")
               : Ok(board);
        }
    }
}