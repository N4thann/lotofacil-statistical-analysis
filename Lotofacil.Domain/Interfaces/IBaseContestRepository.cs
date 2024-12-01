using Lotofacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Domain.Interfaces
{
    public interface IBaseContestRepository
    {
        Task<IEnumerable<BaseContest>> GetAllWithContestsAbove11Async();
    }
}
