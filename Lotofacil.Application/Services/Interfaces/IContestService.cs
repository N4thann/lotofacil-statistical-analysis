using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.Services.Interfaces
{
    public interface IContestService
    {
        Task CreateAsync(ContestViewModel contestVM);
        Task<IEnumerable<Contest>> GetAllContestAsync();
    }
}
