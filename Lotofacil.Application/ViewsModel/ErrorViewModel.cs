using Lotofacil.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Lotofacil.Application.ViewsModel
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string ?message, string ?excepetiondetails, int errorTypeCode)
        {
            Message = message;
            ExceptionDetails = excepetiondetails;
            ErrorType = MapErrorType(errorTypeCode);
        }

        public string ?Message { get; private set; }
        public string ?ExceptionDetails { get; private set; }

        public ErrorType ErrorType { get; private set; }

        private ErrorType MapErrorType (int errorTypeCode)
        {
            return errorTypeCode switch
            {
                1 => ErrorType.Critical,
                2 => ErrorType.NoRecords,
                3 => ErrorType.NotFound,
                _ => ErrorType.Unspecified,
            };
        }
    }
}
