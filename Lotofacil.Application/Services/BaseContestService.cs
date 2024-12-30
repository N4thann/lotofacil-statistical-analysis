using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.Services
{
    public class BaseContestService : IBaseContestService
    {
        private readonly IRepository<BaseContest> _repository;
        private readonly IContestManagementService _contestMS;
        private readonly IBaseContestRepository _repositoryBC;
        public BaseContestService(IRepository<BaseContest> repository, 
            IContestManagementService contestMS,
            IBaseContestRepository repositoryBC)
        {
            _repository = repository;
            _contestMS = contestMS;
            _repositoryBC = repositoryBC;
        }

        public async Task<IEnumerable<BaseContest>> GetAllBaseContestAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateAsync(ContestViewModel contestVM)
        {
            var formattedName = $"Concurso {contestVM.Name}";
            //Deve ser atribuído assim, visto que o objeto só pode ser criado passando os parâmetros para o construtor
            var baseContest = new BaseContest(formattedName, _contestMS.SetDataHour(contestVM.Data), _contestMS.FormatNumbersToSave(contestVM.Numbers));

            await _repository.AddAsync(baseContest);//O método AddAsync do repositório já é assíncrono, então precisamos await essa chamada.
        }
        public async Task EditBaseContestAsync(ContestViewModel contestVM)
        {
            var baseContest = await _repository.GetByIdAsync(contestVM.Id);
            baseContest.Name = contestVM.Name; 
            baseContest.Data = _contestMS.SetDataHour(contestVM.Data);
            baseContest.Numbers = _contestMS.FormatNumbersToSave(contestVM.Numbers);

            await _repository.UpdateAsync(baseContest);
        }
        //Esse método serve para recuperar uma BaseContest para ser mostrado na tela, de forma que respeite as dependencias da camada Presentation
        public async Task<ContestViewModel> ShowOnScreen(int id)
        {
            var baseContest = await _repository.GetByIdAsync(id);

            if (baseContest == null)
                throw new KeyNotFoundException($"Concurso com ID {id} não encontrado.");

            return new ContestViewModel
            {
                Id = baseContest.Id,
                Name = baseContest.Name,
                Data = baseContest.Data,
                Numbers = baseContest.Numbers
            };
        }

        public async Task<IEnumerable<BaseContest>> GetAllWithContestsAbove11Async()
        {
            return await _repositoryBC.GetAllWithContestsAbove11Async();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var baseContest = await _repository.GetByIdAsync(id);
            if (baseContest == null)
                throw new KeyNotFoundException($"Concurso com ID {id} não encontrado.");

            await _repository.DeleteAsync(id);
        }

        public IQueryable<BaseContest> GetQueryableBaseContests()
        {
            return _repository.GetAllQueryable();
        }

        public async Task<List<BaseContest>> GetFilteredBaseContestsAsync(string? name, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize)
        {
            var query = GetQueryableBaseContests();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(log => log.Name.Contains(name));

            if (startDate.HasValue)
                query = query.Where(log => log.Data >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(log => log.Data <= endDate.Value);

            return await query
                .OrderBy(log => log.Data)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string? name, DateTime? startDate, DateTime? endDate)
        {
            var query = GetQueryableBaseContests();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(log => log.Name.Contains(name));

            if (startDate.HasValue)
                query = query.Where(log => log.Data >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(log => log.Data <= endDate.Value);

            return await query.CountAsync();
        }
    }
}
