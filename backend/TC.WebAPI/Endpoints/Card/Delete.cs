using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Card
{
    [Route("api/cards")]
    public class Delete(ICardService cardService)
    : EndpointBaseAsync
            .WithRequest<int>
            .WithActionResult<bool>
    {
        private readonly ICardService _cardService = cardService;

        [HttpDelete("{id}")]
        public override async Task<ActionResult<bool>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            bool isDeleted = await _cardService.DeleteCardAsync(id, cancellationToken);

            return isDeleted
                ? Ok("Card deleted successfully.")
                : BadRequest("Failed to delete the card. The card may not exist or an error occurred.");
        }
    }
}