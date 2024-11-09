using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.ViewsModel;

namespace Lotofacil.Application.Services
{
    public class ContestManagementService : IContestManagementService
    {
        public string FormatNumbersToSave(string numbers)
        {
            // Converte a string em uma lista de substrings de dois caracteres
            var formattedNumbers = Enumerable
                .Range(0, numbers.Length / 2)
                .Select(i => numbers.Substring(i * 2, 2));

            // Junta as substrings com "-"
            return string.Join("-", formattedNumbers);
        }

        public DateTime SetDataHour(DateTime data)
        {
            return data.Date.AddHours(20);//Todos os concursos devem estar setados para 20 horas
        }

    }
}
