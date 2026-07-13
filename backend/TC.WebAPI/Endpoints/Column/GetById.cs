using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.ColumnDTO;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Column
{
    [Route("api/columns")]
    public class GetById(IColumnService columnService)
    : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<ColumnResponse>
    {
        private readonly IColumnService _columnService = columnService;

        [HttpGet("{id}")]
        public override async Task<ActionResult<ColumnResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            ColumnResponse column = await _columnService.GetColumnByIdAsync(id, cancellationToken);

            return column == null
                ? NotFound("Column not found.")
                : Ok(column);
        }
    }
}