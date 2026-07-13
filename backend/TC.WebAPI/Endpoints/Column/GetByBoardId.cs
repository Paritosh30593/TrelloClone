using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.ColumnDTO;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Column
{
    [Route("api/columns")]
    public class GetByBoardId(IColumnService columnService)
    : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<List<ColumnResponse>>
    {
        private readonly IColumnService _columnService = columnService;

        [HttpGet("board/{boardId}")]
        public override async Task<ActionResult<List<ColumnResponse>>> HandleAsync(int boardId, CancellationToken cancellationToken = default)
        {
            List<ColumnResponse> columns = await _columnService.GetColumnsByBoardIdAsync(boardId, cancellationToken);

            return columns == null || columns.Count == 0
                ? NotFound("No columns found for the specified board.")
                : Ok(columns);
        }
    }
}