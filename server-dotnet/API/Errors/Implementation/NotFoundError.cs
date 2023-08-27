namespace API.Errors.Implementation;

public class NotFoundError : IError
{
    public string Title { get; }
    public string Detail { get; }
    public int StatusCode => StatusCodes.Status404NotFound;

    public NotFoundError(string title, string detail)
    {
        Title = title;
        Detail = detail;
    }
}