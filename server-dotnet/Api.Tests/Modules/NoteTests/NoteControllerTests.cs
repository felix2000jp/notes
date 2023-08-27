using Api.Errors;
using Api.Modules.Note;
using Api.Modules.Note.Dto;
using Api.Modules.Note.Validation;
using Api.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OneOf;
using Xunit;

namespace Api.Tests.Modules.NoteTests;

public class NoteControllerTests
{
    private readonly INoteService _noteService;
    private readonly NoteController _notesController;

    public NoteControllerTests()
    {
        var logger = Substitute.For<ILogger<NoteController>>();

        _noteService = Substitute.For<INoteService>();
        _notesController = new NoteController(logger, _noteService);
    }


    [Fact]
    public async Task Get_OnSuccess_ReturnsOk()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];

        var id = note.Id;

        _noteService.Get(Arg.Any<Guid>()).Returns(note);

        // Act
        var result = (OkObjectResult)await _notesController.Get(id);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        result.Value.Should().BeEquivalentTo(note.ToDto());
    }

    [Fact]
    public async Task Get_OnFailure_ReturnsProblem()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];
        var error = new NotFoundError("Note not found", $"Note with id: {note.Id} could not be found");

        var id = note.Id;
        
        _noteService.Get(Arg.Any<Guid>()).Returns(error);

        // Act
        var result = (ObjectResult)await _notesController.Get(id);

        // Assert
        result.Should().BeOfType<ObjectResult>();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Add_OnSuccess_ReturnsCreated()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];

        var validator = new AddNoteDtoValidator();
        var dto = new AddNoteDto(note.Name, note.Text);

        _noteService.Add(Arg.Any<Note>()).Returns(note);

        // Act
        var result = (CreatedResult)await _notesController.Add(validator, dto);

        // Assert
        result.Should().BeOfType<CreatedResult>();
        result.Value.Should().BeEquivalentTo(note.ToDto());
    }
    
    [Fact]
    public async Task Remove_OnSuccess_ReturnsOk()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];

        var id = note.Id;

        _noteService.Remove(Arg.Any<Guid>()).Returns(note);

        // Act
        var result = (OkObjectResult)await _notesController.Remove(id);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        result.Value.Should().BeEquivalentTo(note.ToDto());
    }

    [Fact]
    public async Task Remove_OnFailure_ReturnsProblem()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];
        var error = new NotFoundError("Note not found", $"Note with id: {note.Id} could not be found");

        var id = note.Id;

        _noteService.Remove(Arg.Any<Guid>()).Returns(error);

        // Act
        var result = (ObjectResult)await _notesController.Remove(id);

        // Assert
        result.Should().BeOfType<ObjectResult>();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task Update_OnSuccess_ReturnsOk()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];

        var validator = new UpdateNoteDtoValidator();
        var dto = new UpdateNoteDto(note.Name, note.Text);
        var id = note.Id;

        _noteService.Update(Arg.Any<Note>()).Returns(note);

        // Act
        var result = (OkObjectResult)await _notesController.Update(validator, dto, id);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        result.Value.Should().BeEquivalentTo(note.ToDto());
    }

    [Fact]
    public async Task Update_OnFailure_ReturnsProblem()
    {
        // Arrange
        var note = NoteFixtures.TestNotes[0];
        var error = new NotFoundError("Note not found", $"Note with id: {note.Id} could not be found");

        var validator = new UpdateNoteDtoValidator();
        var dto = new UpdateNoteDto(note.Name, note.Text);
        var id = note.Id;

        _noteService.Update(Arg.Any<Note>()).Returns(error);

        // Act
        var result = (ObjectResult)await _notesController.Update(validator, dto, id);

        // Assert
        result.Should().BeOfType<ObjectResult>();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}