using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.Services.Interfaces
{
    public interface IBaseContestService
    {
        Task CreateAsync(CreateContestViewModel contestVM);

        Task EditBaseContestAsync(int id, CreateContestViewModel contestVM);

        Task DeleteAsync(int id);

        Task<IEnumerable<BaseContest>> GetAllBaseContestAsync();
    }
}
