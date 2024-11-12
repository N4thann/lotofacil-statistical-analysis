namespace Lotofacil.Domain.Entities
{
    public class Contest : ContestBaseEntity
    {
        public Contest(string name, DateTime data, string numbers)
        {
            Name = name;
            Data = data;
            Numbers = FormatNumbersToSave(numbers);
        }

        public string FormatNumbersToSave(string numbers)
        {
            // Converte a string em uma lista de substrings de dois caracteres
            var formattedNumbers = Enumerable
                .Range(0, numbers.Length / 2)
                .Select(i => numbers.Substring(i * 2, 2));

            // Junta as substrings com "-"
            return string.Join("-", formattedNumbers);
        }
    }
}
