﻿using ClosedXML.Excel;
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
            return data.Date.AddHours(20);//Todos os concursos devem estar setados para 20 horas
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
            return list1.Intersect(list2).Count();
        }

        public MemoryStream GenerateExcelContestActivityLog(IEnumerable<ContestActivityLog> logs)
        {
            //Instalar pacote ClosedXML
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("ContestActivityLogs");

            // Cabeçalhos
            worksheet.Cell(1, 1).Value = "Nome do Concurso";
            worksheet.Cell(1, 2).Value = "Data";
            worksheet.Cell(1, 3).Value = "Números";
            worksheet.Cell(1, 4).Value = "Coincidiu com Base";
            worksheet.Cell(1, 5).Value = "Nome Base";
            worksheet.Cell(1, 6).Value = "Números Base";
            worksheet.Cell(1, 7).Value = "Data Criação";

            // Adicionar registros
            var row = 2;
            foreach (var log in logs)
            {
                worksheet.Cell(row, 1).Value = log.Name;
                worksheet.Cell(row, 2).Value = log.Data.ToString("yyyy-MM-dd");
                worksheet.Cell(row, 3).Value = log.Numbers;
                worksheet.Cell(row, 4).Value = log.MatchedAnyBaseContest ? "Sim" : "Não";
                worksheet.Cell(row, 5).Value = log.BaseContestName ?? "N/A";
                worksheet.Cell(row, 6).Value = log.BaseContestNumbers ?? "N/A";
                worksheet.Cell(row, 7).Value = log.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
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
