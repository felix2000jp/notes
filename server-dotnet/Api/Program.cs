using Api.Config;
using Api.Data;
using Api.Extensions;
using Api.Modules.Note;
using Api.Modules.Note.Dto;
using Api.Modules.Note.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    var databaseConfig = builder.Configuration.GetConfig<DatabaseConfig>();


    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseMySql(databaseConfig.TestSource, ServerVersion.AutoDetect(databaseConfig.TestSource));
    });
    
    builder.Services.AddScoped<IValidator<AddNoteDto>, AddNoteDtoValidator>();
    builder.Services.AddScoped<IValidator<UpdateNoteDto>, UpdateNoteDtoValidator>();
    builder.Services.AddScoped<INoteService, NoteService>();

    builder.Services.AddControllers();
}

var app = builder.Build();
{
    var securityConfig = builder.Configuration.GetConfig<SecurityConfig>();

    app.MigrateDatabase();

    app.UseCors(options =>
    {
        options.WithOrigins(securityConfig.TestHosts).AllowCredentials().AllowAnyHeader().AllowAnyMethod();
    });
    
    app.MapControllers();
    app.Run();
}