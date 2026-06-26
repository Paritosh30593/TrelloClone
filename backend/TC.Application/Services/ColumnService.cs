using TC.Application.ServiceContracts.ColumnAggregate;
using TC.Application.RespositoryContracts;

namespace TC.Application.Services
{
    public class ColumnService : IColumnUpdaterService, IColumnGetterService, IColumnDeleterService, IColumnAdderService
    {
        private readonly IColumnRepository _columnRepository;

        public ColumnService(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository;
        }

    }
}