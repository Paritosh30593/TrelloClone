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
    public class GetAll(IBoardService boardService)
    : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<BoardResponse>>
    {
        private readonly IBoardService _boardService = boardService;

        [HttpGet]
        public override async Task<ActionResult<List<BoardResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            List<BoardResponse> boards = await _boardService.GetAllBoardsAsync(cancellationToken);

            return boards == null || boards.Count == 0
                ? NotFound("No boards found.")
                : Ok(boards);
        }
    }
}