namespace Book_manager.src.BookManager.Application.Common.Responses;

public class CommandResponse : BaseResponse
{
    public object? Data { get; set; }

    public static CommandResponse Ok(object? data = null, string? message = null)
        => new() { Success = true, Data = data, Message = message ?? "Operação realizada com sucesso." };

    public static CommandResponse Fail(string message)
        => new() { Success = false, Message = message };
}
