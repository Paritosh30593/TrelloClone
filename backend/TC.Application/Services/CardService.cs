using TC.Application.RespositoryContracts;
using TC.Application.ServiceContracts;

namespace TC.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
    }
}