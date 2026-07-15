using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Board
{
    [Route("api/boards")]
    public class Delete(IBoardService boardService)
    : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult
    {
        private readonly IBoardService _boardService = boardService;

        [HttpDelete("{id}")]
        public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            bool isDeleted = await _boardService.DeleteBoardAsync(id, cancellationToken);

            return isDeleted
                ? Ok("Board deleted successfully.")
                : BadRequest("Failed to delete the board. The board may not exist or an error occurred.");
        }
    }
}