using Api.Modules.Note.Dto;

namespace Api.Modules.Note;

public class Note
{
    public Guid Id { get; }
    public string Name { get; set; }
    public string Text { get; set; }

    public Note(Guid id, string name, string text)
    {
        Id = id;
        Name = name;
        Text = text;
    }


    public NoteDto ToDto()
    {
        return new NoteDto(Id, Name, Text);
    }

    public static Note From(CreateNoteDto dto)
    {
        return new Note(Guid.NewGuid(), dto.Name, dto.Text);
    }

    public static Note From(UpdateNoteDto dto, Guid id)
    {
        return new Note(id, dto.Name, dto.Text);
    }
}