using Application.DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Contracts
{
    public interface IProvidersRepository : IGenericRepository<Providers>
    {
        /// <summary>
        /// Find provider by their phonenumber
        /// </summary>
        /// <param name="TelNo"></param>
        /// <returns></returns>
        Task<Providers?> FindByTelNoAsync(string TelNo);
    }
}
