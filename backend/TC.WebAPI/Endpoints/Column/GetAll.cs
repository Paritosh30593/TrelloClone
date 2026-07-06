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
    public class GetAll(IColumnService columnService)
    : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<ColumnResponse>>
    {
        private readonly IColumnService _columnService = columnService;

        [HttpGet]
        public override async Task<ActionResult<List<ColumnResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            List<ColumnResponse> columns = await _columnService.GetAllColumnsAsync(cancellationToken);

            return columns == null
                ? NotFound()
                : Ok(columns);
        }
    }
}