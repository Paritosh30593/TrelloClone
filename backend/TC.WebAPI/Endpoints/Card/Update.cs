using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.CardDTO;
using TC.Application.ServiceContracts;
using System.Threading;

namespace TC.WebAPI.Endpoints.Card
{
    [Route("api/cards")]
    public class Update(ICardService cardService)
    : EndpointBaseAsync
        .WithRequest<CardUpdateRequest>
        .WithActionResult<CardResponse>
    {
        private readonly ICardService _cardService = cardService;

        [HttpPut]
        public override async Task<ActionResult<CardResponse>> HandleAsync(CardUpdateRequest request, CancellationToken cancellationToken = default)
        {
            CardResponse updatedCard = await _cardService.UpdateCardAsync(request, cancellationToken);

            return updatedCard == null
                ? BadRequest("Failed to update the card. Please check the request data.")
                : Ok(updatedCard);
        }
    }
}