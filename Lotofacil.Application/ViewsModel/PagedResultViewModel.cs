﻿using Lotofacil.Domain.Entities;


namespace Lotofacil.Application.ViewsModel
{
    public class PagedResultViewModel
    {
        public List<ContestActivityLog> Logs { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        // Filtros
        public string? NameFilter { get; set; }
        public DateTime? StartDateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }
    }
}