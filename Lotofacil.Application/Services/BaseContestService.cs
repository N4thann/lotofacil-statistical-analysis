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

        public async Task CreateAsync(CreateContestViewModel contestVM)
        {
            //Deve ser atribuído assim, visto que o objeto só pode ser criado passando os parâmetros para o construtor
            var baseContest = new BaseContest(contestVM.Name, _contestMS.SetDataHour(contestVM.Data), _contestMS.FormatNumbersToSave(contestVM.Numbers));

            await _repository.AddAsync(baseContest);//O método AddAsync do repositório já é assíncrono, então precisamos await essa chamada.
        }
    }
}
