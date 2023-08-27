namespace Api.Errors.Concrete;

public class NotFoundError : IError
{
    public string Title { get; }
    public string Detail { get; }
    public int StatusCode { get; }

    public NotFoundError(string title, string detail)
    {
        Title = title;
        Detail = detail;
        StatusCode = StatusCodes.Status404NotFound;
    }
}