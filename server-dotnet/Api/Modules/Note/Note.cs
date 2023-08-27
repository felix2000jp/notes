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

}