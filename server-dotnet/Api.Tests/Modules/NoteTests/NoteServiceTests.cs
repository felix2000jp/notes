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
    public async Task Select_OnSuccess_ReturnsNote()
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
    public async Task Select_OnFailure_ReturnsIError()
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
}