using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TC.Application.DTO.CardDTO;
using TC.Application.Helpers;
using TC.Application.RespositoryContracts;
using TC.Application.ServiceContracts;
using TC.Domain.Entities;

namespace TC.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly DefaultCardsOptions _defaultCardsOptions;

        public CardService(ICardRepository cardRepository, IOptions<DefaultCardsOptions> defaultCardsOptions)
        {
            _cardRepository = cardRepository;
            _defaultCardsOptions = defaultCardsOptions.Value;
        }

        #region Getters
        public async Task<List<CardResponse>> GetAllCardsAsync(CancellationToken cancellationToken = default)
        {
            return (await _cardRepository.GetAllCardsAsync(cancellationToken))
                .Select(c => c.ToCardResponse())
                .ToList();
        }

        public async Task<List<CardResponse>> GetCardsByColumnIdAsync(int columnId, CancellationToken cancellationToken = default)
        {
            if (columnId <= 0)
            {
                return new List<CardResponse>();
            }
            return (await _cardRepository.GetCardsByColumnIdAsync(columnId, cancellationToken))
                .Select(c => c.ToCardResponse())
                .ToList();
        }

        public async Task<CardResponse> GetCardByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                return null;
            }
            return (await _cardRepository.GetCardByIdAsync(id, cancellationToken))?.ToCardResponse();
        }
        #endregion


        #region Adders
        public async Task<CardResponse> AddCardAsync(CardAddRequest cardRequest, CancellationToken cancellationToken = default)
        {
            if (cardRequest == null || cardRequest.ColumnId <= 0 || string.IsNullOrWhiteSpace(cardRequest.Title))
            {
                throw new ArgumentNullException(nameof(cardRequest), "CardAddRequest cannot be null.");
            }

            Card card = cardRequest.ToCard();

            card = await _cardRepository.AddCardAsync(card, cancellationToken)
                ?? throw new InvalidOperationException("Card cannot be null.");

            return card.ToCardResponse();
        }

        public async Task<int> AddDefaultCardsToColumnAsync(int columnId, CancellationToken cancellationToken = default)
        {
            if (columnId <= 0)
            {
                throw new ArgumentNullException(nameof(columnId), "ColumnId cannot be null or less than or equal to zero.");
            }

            List<Card> defaultCards = _defaultCardsOptions.Columns
                .Select(c => new CardAddRequest
                {
                    ColumnId = columnId,
                    Title = c.Title,
                    SortOrder = c.SortOrder
                }.ToCard())
                .ToList();

            return await _cardRepository.AddDefaultCardsToColumnAsync(defaultCards, cancellationToken);
        }
        #endregion


        #region Updaters
        public async Task<CardResponse> UpdateCardAsync(CardUpdateRequest cardRequest, CancellationToken cancellationToken = default)
        {
            if (cardRequest == null || cardRequest.Id <= 0 || string.IsNullOrWhiteSpace(cardRequest.Title))
            {
                throw new ArgumentNullException(nameof(cardRequest), "CardUpdateRequest cannot be null.");
            }

            Card card = cardRequest.ToCard();

            card = await _cardRepository.UpdateCardAsync(card, cancellationToken)
                ?? throw new InvalidOperationException("Card cannot be null.");

            return card.ToCardResponse();
        }
        #endregion


        #region Deleters
        public async Task<bool> DeleteCardAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Card ID must be greater than zero.");
            }

            return await _cardRepository.DeleteCardAsync(id, cancellationToken);
        }
        #endregion
    }
}