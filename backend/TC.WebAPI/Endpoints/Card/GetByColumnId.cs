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
    public class GetByColumnId(ICardService cardService)
    : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<List<CardResponse>>
    {
        private readonly ICardService _cardService = cardService;

        [HttpGet("column/{columnId}")]
        public override async Task<ActionResult<List<CardResponse>>> HandleAsync(int columnId, CancellationToken cancellationToken = default)
        {
            List<CardResponse> cards = await _cardService.GetCardsByColumnIdAsync(columnId, cancellationToken);

            return cards == null || cards.Count == 0
                ? NotFound("No cards found for the specified column.")
                : Ok(cards);
        }
    }
}