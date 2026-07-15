using System.Data.Common;

namespace TC.Application.RespositoryContracts.Common
{
    public interface IDapperUnitOfWork : IUnitOfWork
    {
        DbConnection DBConnection { get; }
        DbTransaction DBTransaction { get; }
    }
}