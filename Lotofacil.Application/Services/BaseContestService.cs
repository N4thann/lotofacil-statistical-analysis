using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Lotofacil.Application.Services
{
    public class BaseContestService : IBaseContestService
    {
        private readonly IRepository<BaseContest> _repository;
        private readonly IContestManagementService _contestMS;
        private readonly IBaseContestRepository _repositoryBC;
        private readonly IContestActivityLogService _activityLS;
        public BaseContestService(IRepository<BaseContest> repository, 
            IContestManagementService contestMS,
            IBaseContestRepository repositoryBC,
            IContestActivityLogService activityLS)
        {
            _repository = repository;
            _contestMS = contestMS;
            _repositoryBC = repositoryBC;
            _activityLS = activityLS;
        }

        public async Task<IEnumerable<BaseContest>> GetAllBaseContestAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateAsync(ContestViewModel contestVM)
        {
            var formattedName = $"Concurso {contestVM.Name}";
            var contestLog = Log.ForContext("ContestName", contestVM.Name);
            contestLog.Debug("String de nome do Concurso formatada: {FormattedName}", formattedName);
            //Deve ser atribuído assim, visto que o objeto só pode ser criado passando os parâmetros para o construtor
            var baseContest = new BaseContest(formattedName, _contestMS.SetDataHour(contestVM.Data), _contestMS.FormatNumbersToSave(contestVM.Numbers));

            await _repository.AddAsync(baseContest);//O método AddAsync do repositório já é assíncrono, então precisamos await essa chamada.
            contestLog.Information("Registro criado com sucesso");
        }
        public async Task EditBaseContestAsync(ContestViewModel contestVM)
        {
            var contestLog = Log.ForContext("ConcursoBaseId", contestVM.Id);

            contestLog.Debug("Iniciando edição do Concurso Base");

            var baseContest = await _repository.GetByIdAsync(contestVM.Id);
            if (baseContest == null)
            {
                contestLog.Warning("Tentativa de editar concurso base inexistente.");
                throw new KeyNotFoundException($"Concurso base com ID {contestVM.Id} não encontrado.");
            }

            contestLog.Debug("Atualizando propriedades do concurso base ID {Id}. Nome anterior: {OldName}, Novo nome: {NewName}",
                contestVM.Id, baseContest.Name, contestVM.Name);

            baseContest.Name = contestVM.Name;
            baseContest.Data = _contestMS.SetDataHour(contestVM.Data);
            baseContest.Numbers = _contestMS.FormatNumbersToSave(contestVM.Numbers);

            await _repository.UpdateAsync(baseContest);

            contestLog.Information("Concurso base atualizado com sucesso");
        }
        //Esse método serve para recuperar uma BaseContest para ser mostrado na tela, de forma que respeite as dependencias da camada Presentation
        public async Task<ContestViewModel> ShowOnScreen(int id)
        {
            var contestLog = Log.ForContext("ConcursoBaseId", id);//Conceito de Correlation no Serilog

            contestLog.Debug("Iniciando recuperação do Concurso");

            var baseContest = await _repository.GetByIdAsync(id);

            

            if (baseContest == null)
            {
                contestLog.Warning("Concurso Base não foi encontrado");
                throw new KeyNotFoundException($"Concurso com ID {id} não encontrado.");
            }

            contestLog.Information("Concurso Base recuperado com sucesso");

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
            Log.Debug("Recuperando todos os BaseContests com concursos acima de 11.");

            var result = await _repositoryBC.GetAllWithContestsAbove11Async();

            Log.Information("Recuperação concluída: {Count} concursos encontrados.", result.Count());

            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var baseContest = await _repository.GetByIdAsync(id);

            var contestLog = Log.ForContext("ConcursoBaseId", id);//Conceito de Correlation no Serilog
            contestLog.Debug("Iniciando exclusão do Concurso Base.");

            if (baseContest == null)
            {
                contestLog.Warning("Tentativa de exclusão falhou. Concurso com ID {Id} não encontrado.", id);
                throw new KeyNotFoundException($"Concurso com ID {id} não encontrado.");
            }

            contestLog.Debug("Deletando todas as referências de log associadas ao concurso {Name}.", baseContest.Name);
            await _activityLS.DeleteAllReferencesOfLogByBaseContest(baseContest.Name);

            await _repository.DeleteAsync(id);
            contestLog.Information("Concurso Base excluído com sucesso.");
        }

        public IQueryable<BaseContest> GetQueryableBaseContests()
        {
            Log.Debug("Retornando consulta IQueryable para BaseContests.");
            return _repository.GetAllQueryable();
        }

        public async Task<List<BaseContest>> GetFilteredBaseContestsAsync(string? name, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize)
        {
            Log.Debug("Filtrando BaseContests com os seguintes parâmetros - Nome: {Name}, Data Inicial: {StartDate}, Data Final: {EndDate}, Página: {Page}, Tamanho da Página: {PageSize}",
                name, startDate, endDate, pageNumber, pageSize);

            var query = GetQueryableBaseContests();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(log => log.Name.Contains(name));

            if (startDate.HasValue)
                query = query.Where(log => log.Data >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(log => log.Data <= endDate.Value);

            var result = await query
                .Include(x => x.ContestsAbove11)
                .OrderBy(log => log.Data)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Log.Information("Consulta de BaseContests filtrada retornou {Count} resultados.", result.Count);

            return result;
        }

        public async Task<int> GetTotalCountAsync(string? name, DateTime? startDate, DateTime? endDate)
        {
            Log.Debug("Contando o total de BaseContests com os filtros - Nome: {Name}, Data Inicial: {StartDate}, Data Final: {EndDate}", name, startDate, endDate);

            var query = GetQueryableBaseContests();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(log => log.Name.Contains(name));

            if (startDate.HasValue)
                query = query.Where(log => log.Data >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(log => log.Data <= endDate.Value);

            var count = await query.CountAsync();

            Log.Information("Total de BaseContests encontrados: {Count}", count);
            return count;
        }

    }
}
