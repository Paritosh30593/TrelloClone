using TC.Application.RespositoryContracts;
using TC.Application.ServiceContracts.CardAggregate;

namespace TC.Application.Services
{
    public class CardService : ICardGetterService, ICardUpdaterService, ICardAdderService, ICardDeleterService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
    }
}