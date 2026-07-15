using System;
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
    public class UpdateByColumnId
    : EndpointBaseAsync
            .WithRequest<List<CardUpdateRequest>>
            .WithActionResult<List<CardResponse>>
    {
        private readonly ICardService _cardService;

        public UpdateByColumnId(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPut("bulk")]
        public override async Task<ActionResult<List<CardResponse>>> HandleAsync(List<CardUpdateRequest> cardRequests, CancellationToken cancellationToken = default)
        {
            List<CardResponse> updatedCards = await _cardService.UpdateCardsBulkAsync(cardRequests, cancellationToken);

            return updatedCards == null || updatedCards.Count == 0
                ? BadRequest("Failed to update the cards. Please check the request data.")
                : Ok(updatedCards);
        }
    }
}
