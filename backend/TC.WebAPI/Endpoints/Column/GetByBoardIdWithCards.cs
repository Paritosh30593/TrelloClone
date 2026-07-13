using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TC.Application.ServiceContracts;
using System.Threading;
using TC.Application.DTO.ColumnWithCardsDTO;
using Ardalis.ApiEndpoints;

namespace TC.WebAPI.Endpoints.Column
{
    [Route("api/columns")]
    public class GetByBoardIdWithCards(IColumnService columnService)
    : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<IEnumerable<ColumnWithCardsResponse>>
    {
        private readonly IColumnService _columnService = columnService;

        [HttpGet("board/{boardId}/with-cards")]
        public override async Task<ActionResult<IEnumerable<ColumnWithCardsResponse>>> HandleAsync(int boardId, CancellationToken cancellationToken = default)
        {
            List<ColumnWithCardsResponse> columns = await _columnService.GetBoardColumnsWithCardsAsync(boardId, cancellationToken);

            return columns == null || columns.Count == 0
                ? NotFound("No columns found for the specified board.")
                : Ok(columns);
        }
    }
}
