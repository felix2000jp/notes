using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Note;

[Route("api/notes")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly ILogger<NoteController> _logger;
    private readonly INoteService _noteService;

    public NoteController(ILogger<NoteController> logger, INoteService noteService)
    {
        _noteService = noteService;
        _logger = logger;
    }
}