using System.Collections.Generic;

namespace Yupi.Domain.Models.Exceptions
{
    public class ValidationErrorException
    {
        public List<ValidationErrorExceptionErrorModel> Errors { get; set; } = new List<ValidationErrorExceptionErrorModel>();
    }
}