using Catalog.Context;
using Catalog.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

//variável usada para inicializar e configurar a nossa aplicação.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<AppDbContext>(op =>
{
    op.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddControllers().AddJsonOptions(op =>
{
    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//aqui iniciamos a nossa aplicação com base nas informações passadas nos serviços acima.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //aqui adicionamos o nosso middleware personalizado para tratamento de erros
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); 

app.Run();
