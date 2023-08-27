using Api.Extensions;
using Api.Modules.Note.Dto;
using FluentValidation;
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


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        // Service
        var result = await _noteService.Get(id);

        // Response
        return result.Match(
            value =>
            {
                _logger.Log(LogLevel.Information, "Success selecting note with id: {Id}", id);
                return Ok(value.ToDto());
            },
            error =>
            {
                _logger.Log(LogLevel.Error, "Failure selecting note with id: {Id}", id);
                return Problem(title: error.Title, detail: error.Detail, statusCode: error.StatusCode);
            });
    }

    [HttpPost]
    public async Task<IActionResult> Add(IValidator<AddNoteDto> validator, AddNoteDto dto)
    {
        // Validation
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return ValidationProblem(validationResult.ToModelStateDictionary());
        }

        // Service
        var note = Note.From(dto);
        var result = await _noteService.Add(note);

        // Response
        return result.Match(
            value =>
            {
                _logger.Log(LogLevel.Information, "Success creating note: {Note}", dto);
                return Created($"http://localhost:8080/api/v1/notes/{value.Id}", value.ToDto());
            },
            error =>
            {
                _logger.Log(LogLevel.Error, "Failure creating note: {Note}", dto);
                return Problem(title: error.Title, detail: error.Detail, statusCode: error.StatusCode);
            });
    }
}