namespace Core.Shared
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ApiResponse(T data)
        {
            Success = true;
            Data = data;
        }

        public ApiResponse(bool status,string message)
        {
            Success = false;
            Message = message;
        }
    }
}