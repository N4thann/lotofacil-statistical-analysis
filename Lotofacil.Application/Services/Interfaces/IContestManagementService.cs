using Lotofacil.Domain.Entities;
using System.Text;

namespace Lotofacil.Application.Services.Interfaces
{
    /// <summary>
    /// Service responsible for contest management operations.
    /// </summary>
    public interface IContestManagementService
    {
        /// <summary>
        /// Sets the hour of the provided DateTime to 20:00 (8 PM).
        /// </summary>
        /// <param name="data">The DateTime to be adjusted.</param>
        /// <returns>A DateTime instance with the time set to 20:00.</returns>
        DateTime SetDataHour(DateTime data);

        /// <summary>
        /// Converts a formatted string of numbers separated by hyphens into a list of integers.
        /// </summary>
        /// <param name="input">The formatted string (e.g., "01-02-03").</param>
        /// <returns>A list of integers extracted from the string.</returns>
        /// <exception cref="FormatException">Thrown when the input string contains non-numeric values.</exception>
        List<int> ConvertFormattedStringToList(string input);

        /// <summary>
        /// Formats a string of digits into a hyphen-separated string with pairs of digits.
        /// </summary>
        /// <param name="input">The input string of digits (e.g., "010203").</param>
        /// <returns>A formatted string (e.g., "01-02-03").</returns>
        /// <exception cref="ArgumentException">Thrown when the input string length is odd or empty.</exception>
        string FormatNumbersToSave(string input);

        /// <summary>
        /// Calculates the number of matching elements between two integer lists.
        /// </summary>
        /// <param name="list1">The first list of integers.</param>
        /// <param name="list2">The second list of integers.</param>
        /// <returns>The count of matching integers.</returns>
        int CalculateIntersection(List<int> list1, List<int> list2);

        /// <summary>
        /// Generates an Excel file containing contest activity logs.
        /// </summary>
        /// <param name="data">The collection of contest activity logs to include in the file.</param>
        /// <returns>A memory stream containing the generated Excel file.</returns>
        MemoryStream GenerateExcelForContestActivityLog(IEnumerable<ContestActivityLog> data);

        /// <summary>
        /// Generates an Excel file containing base contest information.
        /// </summary>
        /// <param name="data">The collection of base contests to include in the file.</param>
        /// <returns>A memory stream containing the generated Excel file.</returns>
        MemoryStream GenerateExcelForBaseContest(IEnumerable<BaseContest> data);        
    }   
}
/*
 Não adicione modificadores como static, private, ou protected em métodos de interface, 
exceto em casos especiais  para métodos estáticos com implementação (C# 8.0 ou superior).*/
