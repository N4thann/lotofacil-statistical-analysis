using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
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
        public BaseContestService(IRepository<BaseContest> repository, IContestManagementService contestMS)
        {
            _repository = repository;
            _contestMS = contestMS;
        }

        public async Task<IEnumerable<BaseContest>> GetAllBaseContestAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateAsync(ContestViewModel contestVM)
        {
            //Deve ser atribuído assim, visto que o objeto só pode ser criado passando os parâmetros para o construtor
            var baseContest = new BaseContest(contestVM.Name, _contestMS.SetDataHour(contestVM.Data), _contestMS.FormatNumbersToSave(contestVM.Numbers));

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

        public async Task DeleteByIdAsync(int id)
        {
            var baseContest = await _repository.GetByIdAsync(id);
            if (baseContest == null)
                throw new KeyNotFoundException($"Concurso com ID {id} não encontrado.");

            await _repository.DeleteAsync(id);
        }
    }
}
