using FluentValidation;
using Lotofacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.Validators
{
    public class ContestValidator : AbstractValidator<ContestBaseEntity>
    {
        public ContestValidator() 
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("O nome é obrigatório.")
                .Length(6, 20).WithMessage("O nome deve ter entre 6 e 20 caracteres.");
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