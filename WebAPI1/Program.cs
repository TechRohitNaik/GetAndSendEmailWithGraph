using WebAPI1.Application;
using WebAPI1.Domain.Models;
using WebAPI1.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<GraphConfig>(Configuration.GetSection(nameof(GraphConfig)));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddInfrastructure(Configuration);

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
