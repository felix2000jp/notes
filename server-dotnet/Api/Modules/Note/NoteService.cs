using Api.Data;
using Api.Errors;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Api.Modules.Note;

public class NoteService : INoteService
{
    private readonly DataContext _dataContext;

    public NoteService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    
    public async Task<OneOf<Note, IError>> Get(Guid id)
    {
        var noteToSearch = await _dataContext.Notes.SingleOrDefaultAsync(n => n.Id == id);
        if (noteToSearch is null)
        {
            return new NotFoundError("Note not found", $"Note with id: {id} could not be found");
        }

        return noteToSearch;
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