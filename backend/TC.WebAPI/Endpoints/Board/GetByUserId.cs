using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.BoardDTO;
using TC.Application.ServiceContracts;
using Ardalis.ApiEndpoints;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace TC.WebAPI.Endpoints.Board
{
    [Route("api/boards")]
    public class GetByUserId(IBoardService boardService)
    : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<List<BoardResponse>>
    {
        private readonly IBoardService _boardService = boardService;

        [HttpGet("user/{userId}")]
        public override async Task<ActionResult<List<BoardResponse>>> HandleAsync(string userId, CancellationToken cancellationToken = default)
        {
            List<BoardResponse> boards = await _boardService.GetBoardsByUserIdAsync(userId, cancellationToken);

            return boards == null || boards.Count == 0
                ? NotFound("No boards found for the specified user.")
                : Ok(boards);
        }
    }
}