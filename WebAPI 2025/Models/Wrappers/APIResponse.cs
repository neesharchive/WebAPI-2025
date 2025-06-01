namespace WebAPI_2025.Models.Wrappers
{
    public class APIResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public APIResponse() { }
        public APIResponse(bool success, string message, T data)
        {
            Message = message;
            Success = success;
            Data = data;
        }
        public APIResponse(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = default;
        }
    }
}
