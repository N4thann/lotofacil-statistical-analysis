using Lotofacil.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Lotofacil.Application.ViewsModel
{
    public class ErrorViewModel
    {
        public string ?Message { get; set; }
        public string ?ExceptionDetails { get; set; }

        public ErrorType ErrorType { get; set; }
    }
}
