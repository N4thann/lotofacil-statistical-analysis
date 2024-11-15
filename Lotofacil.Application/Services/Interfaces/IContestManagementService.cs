using System.Text;

namespace Lotofacil.Application.Services.Interfaces
{
    public interface IContestManagementService
    {
        public DateTime SetDataHour(DateTime data);

        public List<int> ConvertFormattedStringToList(string input);

        public string FormatNumbersToSave(string input);
    }   
}
