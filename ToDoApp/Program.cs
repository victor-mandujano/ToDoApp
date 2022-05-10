using Microsoft.OpenApi.Models;
using ToDoApp.Extensions;

// Pass 'useEFInMemory' as a command line argument to use Entity Framework Core InMemory database.
// Otherwise, a Dictionary store will be used.
var useEFInMemory = args.Contains("useEFInMemory");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterRepositories(useEFInMemory);
builder.Services.RegisterServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((options) =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Todo API",
        Description = "A simple API for managing Todo items."
    });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ToDoApp.xml"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
