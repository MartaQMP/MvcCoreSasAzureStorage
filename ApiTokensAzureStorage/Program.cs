using ApiTokensAzureStorage.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddTransient<ServiceSasToken>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.MapOpenApi();
app.MapScalarApiReference();
app.UseHttpsRedirection();


// NECESITAMOS MAPEAR NUESTRO METODO token DENTRO DE UN ENDPOINT
app.MapGet("/token/{curso}", (string curso, ServiceSasToken service) =>
{
    string token = service.GenerateToken(curso);
    return new { token = token };
});


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
