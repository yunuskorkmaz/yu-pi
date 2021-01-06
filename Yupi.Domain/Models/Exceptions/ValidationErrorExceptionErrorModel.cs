namespace Yupi.Domain.Models.Exceptions
{
    public class ValidationErrorExceptionErrorModel
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}