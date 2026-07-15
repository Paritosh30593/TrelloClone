using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.CardDTO;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Card
{
    [Route("api/cards")]
    public class GetAll(ICardService cardService)
    : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<CardResponse>>
    {
        private readonly ICardService _cardService = cardService;

        [HttpGet]
        public override async Task<ActionResult<List<CardResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            List<CardResponse> cards = await _cardService.GetAllCardsAsync(cancellationToken);

            return cards == null || cards.Count == 0
                ? NotFound("No cards found.")
                : Ok(cards);
        }
    }
}