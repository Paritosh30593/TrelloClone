using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Column
{
    [Route("api/columns")]
    public class Delete(IColumnService columnService)
    : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<bool>
    {
        private readonly IColumnService _columnService = columnService;

        [HttpDelete("{id}")]
        public override async Task<ActionResult<bool>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            bool isDeleted = await _columnService.DeleteColumnAsync(id, cancellationToken);

            return isDeleted
                ? Ok("Column deleted successfully.")
                : BadRequest("Failed to delete the column. The column may not exist or an error occurred.");
        }
    }
}