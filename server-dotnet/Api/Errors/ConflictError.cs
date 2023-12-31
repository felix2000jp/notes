namespace Api.Errors;

public class ConflictError : IError
{
    public string Title { get; }
    public string Detail { get; }
    public int StatusCode { get; }

    public ConflictError(string title, string detail)
    {
        Title = title;
        Detail = detail;
        StatusCode = StatusCodes.Status409Conflict;
    }
}