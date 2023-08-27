using Api.Modules.Note;

namespace Api.Tests.Fixtures;

public struct NoteFixtures
{
    public static List<Note> TestNotes => new()
    {
        new Note(Guid.NewGuid(), "Note 1", "Note 1 - Text"),
        new Note(Guid.NewGuid(), "Note 2", "Note 2 - Text"),
        new Note(Guid.NewGuid(), "Note 3", "Note 3 - Text"),
        new Note(Guid.NewGuid(), "Note 4", "Note 4 - Text"),
        new Note(Guid.NewGuid(), "Note 5", "Note 5 - Text"),
    };
}