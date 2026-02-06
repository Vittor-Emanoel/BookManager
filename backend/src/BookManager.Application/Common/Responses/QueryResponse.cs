namespace Book_manager.src.BookManager.Application.Common.Responses;

public class QueryResponse<T> : BaseResponse
{
	public T? Data { get; set; }

	public static QueryResponse<T> Ok(T data, string? message = null)
		=> new() { Success = true, Data = data, Message = message ?? "Consulta realizada com sucesso." };

	public static QueryResponse<T> Fail(string message)
		=> new() { Success = false, Message = message };
}
