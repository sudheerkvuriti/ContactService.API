namespace ContactService.API.Models
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public ApiErrorResponse(string message, List<string>? errors = null)
        {
            Message = message;
            Errors = errors ?? new List<string>();
        }
    }
}
