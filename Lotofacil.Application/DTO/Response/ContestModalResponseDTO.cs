using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.DTO.Response
{
    public record ContestModalResponseDTO(
        double EvenNumbersAveragePercentage,
        double OddNumbersAveragePercentage,
        List<int> Top5MostFrequentNumbers,
        List<int> Top5LeastFrequentNumbers,
        double MultiplesOfThreeAveragePercentage);
}
