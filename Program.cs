using Letrasdemusicas.Data;
using Letrasdemusicas.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConetion")));

var app = builder.Build();

app.MapGet("/", () => "API está funcionando!");

app.MapPost("/musicas", async (Musica musica, AppDbContext dbContext) =>
{
    dbContext.Musicas.Add(musica);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/musicas/{musica.Id}", musica);
});

app.Run();
