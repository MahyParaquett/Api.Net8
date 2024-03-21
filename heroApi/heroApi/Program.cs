using Repository.Connection;
using Repository.Contracts;
using Repository.Repositories;
using Service.Contracts;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IHeroService, HeroService>();
builder.Services.AddSingleton<IHeroRepository, HeroRepository>();
builder.Services.AddSingleton<IConnectionFactory, DefaultSqlConnectionFactory>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
