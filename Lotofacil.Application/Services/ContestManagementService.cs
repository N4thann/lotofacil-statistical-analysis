using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Lotofacil.Application.Services
{
    public class ContestManagementService : IContestManagementService
    {
        public DateTime SetDataHour(DateTime data)
        {
            return data.Date.AddHours(20);
        }

        public List<int> ConvertFormattedStringToList(string input)
        {
            List<int> numbersList = new List<int>();
            string[] pairs = input.Split('-');

            foreach (string pair in pairs)
            {
                if (int.TryParse(pair, out int number))
                {
                    numbersList.Add(number);
                }
                else
                {
                    throw new FormatException("A string formatada contém valores não numéricos.");
                }
            }

            return numbersList;
        }


        public string FormatNumbersToSave(string input)
        {
            if (input.Length % 2 != 0 || input.Length == 0)
            {
                throw new ArgumentException("A string de entrada deve ter um número par de caracteres e não pode estar vazia.");
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i += 2)
            {
                sb.Append(input.Substring(i, 2));
                if (i < input.Length - 2)
                {
                    sb.Append("-");
                }
            }
            return sb.ToString();
        }

        public int CalculateIntersection(List<int> list1, List<int> list2)
        {
            int matches = 0;

            foreach (var number in list1)
            {
                if (list2.Contains(number))
                {
                    matches++;
                }
            }

            return matches;
        }

        public MemoryStream GenerateExcelForContestActivityLog(IEnumerable<ContestActivityLog> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Log dos Concursos");

            // Definir headers personalizados e sua ordem
            var headers = new List<(string Header, Func<ContestActivityLog, object?> ValueSelector)>
                {
                    ("Concurso", log => log.Name),
                    ("Números", log => log.Numbers),
                    ("Data de Realização", log => log.Data),
                    ("Concurso Base", log => log.BaseContestName),
                    ("Números do Concurso Base", log => log.BaseContestNumbers)
                };

            // Criar cabeçalhos na planilha
            for (int i = 0; i < headers.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i].Header;
            }

            // Estilizar cabeçalhos
            var headerRange = worksheet.Range(1, 1, 1, headers.Count);
            headerRange.Style.Font.FontColor = XLColor.White; // Texto branco
            headerRange.Style.Fill.BackgroundColor = XLColor.Black; // Fundo preto
            headerRange.Style.Font.Bold = true; // Negrito
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//Centralizar os valores

            // Adicionar registros dinamicamente
            int row = 2;
            foreach (var i in data)
            {
                for (int col = 0; col < headers.Count; col++)
                {
                    var value = headers[col].ValueSelector(i); // Obter valor usando o seletor
                    var cell = worksheet.Cell(row, col + 1);
                    cell.Value = value?.ToString() ?? string.Empty;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//Centralizar os valores
                }
                row++;
            }

            // Ajustar largura das colunas
            worksheet.Columns().AdjustToContents();

            // Gerar memória para retorno
            var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }

        public MemoryStream GenerateExcelForBaseContest(IEnumerable<BaseContest> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Concursos Base");

            var headers = new List<(string Header, Func<BaseContest, object?> ValueSelector)>
                {
                    ("Concurso Base", log => log.Name),
                    ("Números", log => log.Numbers),
                    ("Data de Realização", log => log.Data),
                    ("Acertou 11", log => log.Hit11),
                    ("Acertou 12", log => log.Hit12),
                    ("Acertou 13", log => log.Hit13),
                    ("Acertou 14", log => log.Hit14),
                    ("Acertou 15", log => log.Hit15),
                    ("Valor do Cálculo de eficiência", log => 
                    (log.Hit11) + (log.Hit12 * 2) + (log.Hit13 * 3) + (log.Hit14 * 4) * (log.Hit15 * 5)),
                    ("Top 10 números mais frequentes", log => log.TopTenNumbers),
                };

            for (int i = 0; i < headers.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i].Header;
            }

            var headerRange = worksheet.Range(1, 1, 1, headers.Count);
            headerRange.Style.Font.FontColor = XLColor.White;
            headerRange.Style.Fill.BackgroundColor = XLColor.Black; 
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int row = 2;
            foreach (var i in data)
            {
                for (int col = 0; col < headers.Count; col++)
                {
                    var value = headers[col].ValueSelector(i); // Obter valor usando o seletor
                    var cell = worksheet.Cell(row, col + 1);
                    cell.Value = value?.ToString() ?? string.Empty;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }
                row++;
            }

            // Ajustar largura das colunas
            worksheet.Columns().AdjustToContents();

            // Gerar memória para retorno
            var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }


    }
}
