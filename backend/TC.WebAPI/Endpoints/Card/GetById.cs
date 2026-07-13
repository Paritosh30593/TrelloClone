using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.CardDTO;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Endpoints.Card
{
    [Route("api/cards")]
    public class GetById(ICardService cardService)
        : EndpointBaseAsync
            .WithRequest<int>
            .WithActionResult<CardResponse>
    {
        private readonly ICardService _cardService = cardService;

        [HttpGet("{id}")]
        public override async Task<ActionResult<CardResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            CardResponse card = await _cardService.GetCardByIdAsync(id, cancellationToken);

            return card == null
                ? NotFound("Card not found.")
                : Ok(card);
        }
    }
}