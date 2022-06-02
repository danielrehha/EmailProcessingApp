using EmailProcessingApp.API.Helpers;
using EmailProcessingApp.API.Helpers.Contracts;
using EmailProcessingApp.API.Middlewares;
using EmailProcessingApp.Application;
using EmailProcessingApp.Infrastructure;
using EmailProcessingApp.Persistence;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IResponseManager, ResponseManager>();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<HeaderAuthenticationMiddleware>();

app.Run();
