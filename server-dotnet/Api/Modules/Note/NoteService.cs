using Api.Errors;
using OneOf;

namespace Api.Modules.Note.Concrete;

public class NoteService : INoteService
{
    public Task<OneOf<Note, IError>> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<Note, IError>> Add(Note note)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<Note, IError>> Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<Note, IError>> Update(Note note)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<IEnumerable<Note>, IError>> GetAll()
    {
        throw new NotImplementedException();
    }
}