namespace Lotofacil.Application.Services.Interfaces
{
    public interface IContestManagementService
    {
        public DateTime SetDataHour(DateTime data);

        public List<int> ParseFormattedNumbers(string formattedNumbers);
    }
    
}
