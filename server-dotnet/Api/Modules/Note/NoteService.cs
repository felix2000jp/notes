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

    public async Task<OneOf<Note, IError>> Add(Note note)
    {
        _dataContext.Notes.Add(note);
        await _dataContext.SaveChangesAsync();

        return note;
    }

    public async Task<OneOf<Note, IError>> Remove(Guid id)
    {
        var noteToDelete = await _dataContext.Notes.SingleOrDefaultAsync(n => n.Id == id);
        if (noteToDelete is null)
        {
            return new NotFoundError("Note not found", $"Note with id: {id} could not be found");
        }

        _dataContext.Notes.Remove(noteToDelete);
        await _dataContext.SaveChangesAsync();

        return noteToDelete;
    }

    public async Task<OneOf<Note, IError>> Update(Note note)
    {
        var noteToUpdate = await _dataContext.Notes.SingleOrDefaultAsync(n => n.Id == note.Id);
        if (noteToUpdate is null)
        {
            return new NotFoundError("Note not found", $"Note with id: {note.Id} could not be found");
        }

        noteToUpdate.Name = note.Name;
        noteToUpdate.Text = note.Text;
        await _dataContext.SaveChangesAsync();

        return noteToUpdate;
    }

    public async Task<OneOf<IEnumerable<Note>, IError>> GetAll()
    {
        return await _dataContext.Notes.ToListAsync();
    }
}