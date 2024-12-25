using Letrasdemusicas.Data;
using Letrasdemusicas.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConetion")));

builder.Services.AddScoped<IMusicaRepository, MusicaRepository>();

var app = builder.Build();

app.MapGet("/musicas", async (IMusicaRepository repository) =>
{
    var musicas = await repository.GetAllAsync();
    return Results.Ok(musicas);
});

app.MapGet("/musicas/{Id:int}", async(int Id, IMusicaRepository repository) =>
{
    var findMusic = await repository.GetByIdAsync(Id);
    return findMusic is null
    ? Results.NotFound("Musica não cadastrada")
    : Results.Ok(findMusic); 
});

app.MapGet("/musicas/{musicasFiltradas}", async(string musicasFiltradas, IMusicaRepository repository) => 
{
    var search = await repository.SearchAsync(musicasFiltradas);

    return search.Any()
    ? Results.Ok(search)
    : Results.NotFound(new { Message = "Musica não cadastrada" });
});

app.MapPost("/musicas", async (Musica musica, IMusicaRepository repository) =>
{
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(musica, new ValidationContext(musica), validationResults, true);

    if (!isValid)
        return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));

    await repository.AddAsync(musica);
    return Results.Created($"/musicas/{musica.Id}", musica);
});

app.MapDelete("/musicas/{Id}", async (int Id, IMusicaRepository repository) =>
{
    var deleted = await repository.DeleteAsync(Id);
    return deleted? Results.NoContent(): Results.NotFound("Musica não localizada");
});

app.Run();
