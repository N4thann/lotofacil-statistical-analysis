using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Domain.Enums
{
    public enum ErrorType
    {
        Critical, // Erro crítico, geralmente falha de sistema
        NoRecords, // Nenhum registro encontrado
        NotFound, // Recurso não encontrado
        Unspecified
    }
}
