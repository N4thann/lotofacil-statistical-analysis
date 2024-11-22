using Lotofacil.Domain.Entities;
using System.Text;

namespace Lotofacil.Application.Services.Interfaces
{
    public interface IContestManagementService
    {
        DateTime SetDataHour(DateTime data);
        List<int> ConvertFormattedStringToList(string input);
        string FormatNumbersToSave(string input);
        MemoryStream GenerateExcelContestActivityLog(IEnumerable<ContestActivityLog> logs);
    }   
}
/*
 Não adicione modificadores como static, private, ou protected em métodos de interface, 
exceto em casos especiais  para métodos estáticos com implementação (C# 8.0 ou superior).*/
