namespace Api.Errors;

public interface IError
{
    public string Title { get; }
    public string Detail { get; }
    public int StatusCode { get; }
}