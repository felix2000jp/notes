using Api.Data;
using Api.Errors;
using Api.Modules.Note;
using Api.Tests.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Api.Tests.Modules.NoteTests;

public class NoteServiceTests
{
    private readonly DataContext _dataContext;
    private readonly INoteService _noteService;

    public NoteServiceTests()
    {
        var dbName = Guid.NewGuid().ToString();
        var dbOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(dbName).Options;

        _dataContext = new DataContext(dbOptions);
        _noteService = new NoteService(_dataContext);
    }


    [Fact]
    public async Task Get_OnSuccess_ReturnsNote()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];

        var id = note.Id;

        _dataContext.Add(note);
        await _dataContext.SaveChangesAsync();

        // Act
        var result = await _noteService.Get(id);

        // Assert
        result.IsT0.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(note);
    }

    [Fact]
    public async Task Get_OnFailure_ReturnsIError()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];
        var error = new NotFoundError("Note not found", $"Note with id: {note.Id} could not be found");

        var id = note.Id;

        // Act
        var result = await _noteService.Get(id);

        // Assert
        result.IsT1.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(error);
    }

    [Fact]
    public async Task Add_OnSuccess_ReturnsNote()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];

        // Act
        var result = await _noteService.Add(note);

        // Assert
        result.IsT0.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(note);
        _dataContext.Notes.Should().ContainEquivalentOf(note);
    }

    [Fact]
    public async Task Remove_OnSuccess_ReturnsNote()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];

        var id = note.Id;

        _dataContext.Add(note);
        await _dataContext.SaveChangesAsync();

        // Act
        var result = await _noteService.Remove(id);

        // Assert
        result.IsT0.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(note);
        _dataContext.Notes.Should().NotContainEquivalentOf(note);
    }

    [Fact]
    public async Task Remove_OnFailure_ReturnsIError()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];
        var error = new NotFoundError("Note not found", $"Note with id: {note.Id} could not be found");

        var id = note.Id;

        // Act
        var result = await _noteService.Remove(id);

        // Assert
        result.IsT1.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(error);
    }

    [Fact]
    public async Task Update_OnSuccess_ReturnsNote()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];
        var noteToUpdate = new Note(note.Id, "Name to be updated", "Text to be updated");

        _dataContext.Add(noteToUpdate);
        await _dataContext.SaveChangesAsync();

        // Act
        var result = await _noteService.Update(note);

        // Assert
        result.IsT0.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(note);
        _dataContext.Notes.Should().ContainEquivalentOf(note);
    }

    [Fact]
    public async Task Update_OnFailure_ReturnsIError()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];
        var error = new NotFoundError("Note not found", $"Note with id: {note.Id} could not be found");

        // Act
        var result = await _noteService.Update(note);

        // Assert
        result.IsT1.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(error);
    }

    [Fact]
    public async Task GetAll_OnSuccess_ReturnsIEnumerableOfNotes()
    {
        // Arrange
        var notes = NoteFixtures.TestNotes;

        _dataContext.AddRange(notes);
        await _dataContext.SaveChangesAsync();

        // Act
        var result = await _noteService.GetAll();

        // Assert
        result.IsT0.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(notes);
    }
}