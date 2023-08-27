using Api.Config;
using Api.Data;
using Api.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    var databaseConfig = builder.Configuration.GetConfig<DatabaseConfig>();


    builder.Services.AddControllers();

    builder.Services.AddDbContext<DataContext>(options => options.UseMySql(databaseConfig.TestSource, ServerVersion.AutoDetect(databaseConfig.TestSource)));
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}