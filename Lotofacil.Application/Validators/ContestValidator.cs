using FluentValidation;
using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.Validators
{
    public class ContestValidator : AbstractValidator<ContestViewModel>
    {
        private readonly IContestRepository _repository;
        private readonly IBaseContestRepository _baseContestRepository;
        public ContestValidator(IContestRepository repository,
            IBaseContestRepository baseContestRepository) 
        {
            _repository = repository;
            _baseContestRepository = baseContestRepository;

            RuleFor(x => x.Name)
            .NotNull().WithMessage("O nome é obrigatório.")
            .Length(1, 5).WithMessage("O nome do concurso deve ter entre 1 e 5 caracteres.")
            .MustAsync(async (contestVM, name, cancellation) =>
            {
                var formattedName = $"Concurso {name}";

                // Verifica na tabela correta com base no tipo
                if (contestVM.IsBaseContest)
                {
                    return !await _baseContestRepository.ExistsAsync(formattedName);
                }
                else
                {
                    return !await _repository.ExistsAsync(formattedName);
                }
            })
            .WithMessage("Esse concurso já foi cadastrado.");

            RuleFor(x => x.Numbers)
                .NotNull().WithMessage("Os números são obrigatórios.")
                .Length(30).WithMessage("Os números devem ter exatamente 30 caracteres.");

            RuleFor(x => x.Data)
                .NotNull().WithMessage("A data é obrigatória.")
                .Must(data => data != default(DateTime)).WithMessage("A data deve ser válida.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data não pode ser no futuro.");
        }
    }
}
/*.LessThanOrEqualTo(DateTime.Now) - 
 * Permite qualquer data anterior ou igual ao momento atual (precisão até a milissegundos).*/