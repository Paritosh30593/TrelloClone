using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.ColumnDTO;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Column
{
    [Route("api/columns")]
    public class Update(IColumnService columnService)
    : EndpointBaseAsync
        .WithRequest<ColumnUpdateRequest>
        .WithActionResult<ColumnResponse>
    {
        private readonly IColumnService _columnService = columnService;

        [HttpPut]
        public override async Task<ActionResult<ColumnResponse>> HandleAsync(ColumnUpdateRequest columnRequest, CancellationToken cancellationToken = default)
        {
            ColumnResponse column = await _columnService.UpdateColumnAsync(columnRequest, cancellationToken);

            return column == null
                ? BadRequest()
                : Ok(column);
        }
    }
}