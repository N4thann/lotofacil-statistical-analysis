﻿using Lotofacil.Application.DTO.Request;
using Lotofacil.Application.DTO.Response;
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
        Task CreateAsync(ContestViewModel contestVM);

        Task EditBaseContestAsync(ContestViewModel contestVM);

        Task DeleteByIdAsync(int id);

        Task<ContestViewModel> ShowOnScreen(int id);

        Task<IEnumerable<BaseContest>> GetAllBaseContestAsync();

        Task<IEnumerable<BaseContest>> GetAllWithContestsAbove11Async();

        IQueryable<BaseContest> GetQueryableBaseContests();

        Task<List<BaseContest>> GetFilteredBaseContestsAsync(string? name, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize);

        Task<int> GetTotalCountAsync(string? name, DateTime? startDate, DateTime? endDate);

    }
}
