using System.Collections.Generic;

namespace Core.Exceptions
{
    public class ValidationErrorException
    {
        public List<ValidationErrorExceptionErrorModel> Errors { get; set; } = new List<ValidationErrorExceptionErrorModel>();

    }
}