using Api.Errors;
using Api.Modules.Note;
using Api.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
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
}