using Api.Errors;
using OneOf;

namespace Api.Modules.Note;

public interface INoteService
{
    /// <summary>
    /// Gets a note with a given id.
    /// </summary>
    Task<OneOf<Note, IError>> Get(Guid id);

    /// <summary>
    /// Adds a new note.
    /// </summary>
    Task<OneOf<Note, IError>> Add(Note note);

    /// <summary>
    /// Removes a note with a given id.
    /// </summary>
    Task<OneOf<Note, IError>> Remove(Guid id);

    /// <summary>
    /// Updates an existing note.
    /// </summary>
    Task<OneOf<Note, IError>> Update(Note note);
    
    
    /// <summary>
    /// Gets all notes.
    /// </summary>
    Task<OneOf<IEnumerable<Note>, IError>> GetAll();
}