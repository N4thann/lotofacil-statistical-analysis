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
    /// <param name="EvenNumbersAveragePercentage"></param>
    /// <param name="OddNumbersAveragePercentage"></param>
    /// <param name="Top5MostFrequentNumbers">Corresponde aos 5 números mais frequentes</param>
    /// <param name="Top5LeastFrequentNumbers">Corresponde aos 5 números menos frequentes</param>
    /// <param name="MultiplesOfThreeAveragePercentage"></param>
    public record ContestModalResponseDTO(
        double EvenNumbersAveragePercentage,
        double OddNumbersAveragePercentage,
        List<int> Top5MostFrequentNumbers,
        List<int> Top5LeastFrequentNumbers,
        double MultiplesOfThreeAveragePercentage);
}
