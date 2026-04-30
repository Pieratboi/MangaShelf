using MangaShelf.Api.Middleware;
using MangaShelf.Application.Manga.GetAll;
using MangaShelf.Application.Manga;
using MangaShelf.Application.Manga.GetById;
using MangaShelf.Application.Manga.Create;
using MangaShelf.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GetAllMangaUseCase>();
builder.Services.AddScoped<GetMangaByIdUseCase>();
builder.Services.AddScoped<CreateMangaUseCase>();
builder.Services.AddScoped<IMangaRepository, InMemoryMangaRepository>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

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
