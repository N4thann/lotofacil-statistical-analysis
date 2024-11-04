namespace Lotofacil.Application.Services
{
    public class ContestManagementService
    {
        public string FormatNumbersToSave(List<int> numbers)
        {
            return string.Join("-", numbers.Select(n => n.ToString("D2")));
        }
    }
}
