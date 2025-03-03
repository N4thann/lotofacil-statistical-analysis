using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.DTO.Response
{
    /// <summary>
    /// Esse record é uma DTO de response para a tela List de Contests. Faz várias análises após ter sido selecionado múltiplos contests em um checkbox
    /// </summary>
    /// <param name="ContestsName">Corresponde a numeração dos concursos</param>
    /// <param name="EvenNumbersAveragePercentage">Corresponde a porcentagem de numeros pares</param>
    /// <param name="OddNumbersAveragePercentage">Corresponde a porcentagem de numeros ímpares</param>
    /// <param name="Top5MostFrequentNumbers">Corresponde aos 5 números mais frequentes</param>
    /// <param name="Top5LeastFrequentNumbers">Corresponde aos 5 números menos frequentes</param>
    /// <param name="MultiplesOfThreeAveragePercentage">Corresponde a porcentagem de numeros múltiplos de 3</param>
    public record ContestModalResponseDTO(
        List<string> ContestsName,
        double EvenNumbersAveragePercentage,
        double OddNumbersAveragePercentage,
        List<int> Top5MostFrequentNumbers,
        List<int> Top5LeastFrequentNumbers,
        double MultiplesOfThreeAveragePercentage);
}
