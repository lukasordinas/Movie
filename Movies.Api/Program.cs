using Microsoft.EntityFrameworkCore;
using Movies.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Movies.Api.Data.Movies.DbContext>(options =>
options.UseSqlite("Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\Movies\movies.db")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
