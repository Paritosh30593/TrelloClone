using TC.Application.RespositoryContracts;
using TC.Application.ServiceContracts.BoardAggregate;

namespace TC.Application.Services
{
    public class BoardService : IBoardAdderService, IBoardUpdaterService, IBoardGetterService, IBoardDeleterService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

    }
}