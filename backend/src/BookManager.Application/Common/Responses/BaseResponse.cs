namespace Book_manager.src.BookManager.Application.Common.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
