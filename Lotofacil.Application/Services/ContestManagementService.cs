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

        public MemoryStream GenerateExcel<T>(IEnumerable<T> data)
        {
            // Instalar pacote ClosedXML
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Data");

            // Obter propriedades da entidade
            var properties = typeof(T).GetProperties();

            // Criar cabeçalhos dinamicamente
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
            }

            // Estilizar cabeçalhos
            var headerRange = worksheet.Range(1, 1, 1, properties.Length);
            headerRange.Style.Font.FontColor = XLColor.White; // Texto branco
            headerRange.Style.Fill.BackgroundColor = XLColor.Black; // Fundo preto
            headerRange.Style.Font.Bold = true; // Negrito

            // Adicionar registros dinamicamente
            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < properties.Length; col++)
                {
                    var value = properties[col].GetValue(item); // Obter valor da propriedade
                    worksheet.Cell(row, col + 1).Value = value?.ToString() ?? string.Empty;
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
