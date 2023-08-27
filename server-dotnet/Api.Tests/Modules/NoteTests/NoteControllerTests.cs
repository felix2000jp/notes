using Api.Modules.Note;
using Microsoft.Extensions.Logging;
using NSubstitute;

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
}