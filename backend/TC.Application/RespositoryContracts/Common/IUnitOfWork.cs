using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TC.Application.RespositoryContracts
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        Task Save(CancellationToken cancellationToken);
    }
}