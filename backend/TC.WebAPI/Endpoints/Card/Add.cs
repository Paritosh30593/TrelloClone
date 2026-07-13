using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.CardDTO;
using TC.Application.ServiceContracts;
namespace TC.WebAPI.Endpoints.Card
{
    [Route("api/cards")]
    public class Add(ICardService cardService)
    : EndpointBaseAsync
        .WithRequest<CardAddRequest>
        .WithActionResult<CardResponse>
    {
        private readonly ICardService _cardService = cardService;

        [HttpPost]
        public override async Task<ActionResult<CardResponse>> HandleAsync(CardAddRequest cardRequest, CancellationToken cancellationToken = default)
        {
            CardResponse card = await _cardService.AddCardAsync(cardRequest, cancellationToken);

            return card == null
                ? BadRequest("Failed to add the card. Please check the request data.")
                : Ok(card);
        }
    }
}