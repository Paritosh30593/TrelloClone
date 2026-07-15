using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.ColumnDTO;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Column
{
    [Route("api/columns")]
    public class Add(IColumnService columnService)
    : EndpointBaseAsync
        .WithRequest<ColumnAddRequest>
        .WithActionResult<ColumnResponse>
    {
        private readonly IColumnService _columnService = columnService;

        [HttpPost]
        public override async Task<ActionResult<ColumnResponse>> HandleAsync(ColumnAddRequest columnRequest, CancellationToken cancellationToken = default)
        {
            ColumnResponse column = await _columnService.AddColumnAsync(columnRequest, cancellationToken);

            return column == null
                ? BadRequest("Failed to add the column. Please check the request data.")
                : Ok(column);
        }
    }
}