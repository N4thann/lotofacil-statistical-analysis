using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.ViewsModel;

namespace Lotofacil.Application.Services
{
    public class ContestManagementService : IContestManagementService
    {
        public DateTime SetDataHour(DateTime data)
        {
            return data.Date.AddHours(20);//Todos os concursos devem estar setados para 20 horas
        }

        public List<int> ParseFormattedNumbers(string formattedNumbers)
        {
            // Divide a string nos hífens e converte cada parte em um número inteiro
            var numbersList = formattedNumbers
                .Split('-')               // Divide a string em substrings separadas pelo "-"
                .Select(int.Parse)        // Converte cada substring em um número inteiro
                .ToList();                // Converte o resultado para uma lista de inteiros

            return numbersList;
        }

    }
}
