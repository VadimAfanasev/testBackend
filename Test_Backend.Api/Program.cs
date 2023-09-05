using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using TestBackend.Api.Models.Data;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);

try
{
    // Add services to the container.


    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();

    string connection = builder.Configuration.GetConnectionString("DefaultConnection");
    if (String.IsNullOrWhiteSpace(connection))
        connection = "Server=(localdb)\\mssqllocaldb;Database=TestBackend2;Trusted_Connection=True;";
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    LogManager.Shutdown();
}

