using Microsoft.EntityFrameworkCore;
using PersonalWebsiteAPI.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register controller.
builder.Services.AddControllers();

// Register database connection
string stringDatabaseConnection = $"server=localhost;database=PersonalWebsite;userid=root;password=123qweasdzxc;port=3306;default command timeout=300;SslMode=none;AllowPublicKeyRetrieval=true;";
ServerVersion serverVersion = ServerVersion.AutoDetect(stringDatabaseConnection);
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseMySql(stringDatabaseConnection, serverVersion,
    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName))
);

var app = builder.Build();

// configure controller.
app.MapControllers();

// Configure apply migrations.
using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
context.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
