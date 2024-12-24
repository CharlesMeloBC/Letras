using Letrasdemusicas.Data;
using Letrasdemusicas.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConetion")));

var app = builder.Build();

app.MapGet("/musicas", async (AppDbContext dbContext) =>
{
    var musicas = await dbContext.Musicas.ToListAsync();
    return Results.Ok(musicas);
});

app.MapGet("/musicas/{Id:int}", async(int Id, AppDbContext dbContext) =>
{
    var findMusic = await dbContext.Musicas.FindAsync(Id);
    if (findMusic == null)  
    {
        return Results.NotFound("Musica não cadastrada");
    }
       return Results.Ok(findMusic); 
});

app.MapGet("/musicas/{musicasFiltradas}", async(string musicasFiltradas, AppDbContext dbContext) => 
{
    var search = await dbContext.Musicas
    .Where(m => m.Name.ToLower().Contains(musicasFiltradas) || m.Letra.ToLower().Contains(musicasFiltradas))
    .ToListAsync();

if (!search.Any())
   
   {
    return Results.NotFound(new {Message = "Musica não cadastrada" });
   }
    return Results.Ok(search);
});

app.MapPost("/musicas", async (Musica musica, AppDbContext dbContext) =>
{
    dbContext.Musicas.Add(musica);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/musicas/{musica.Id}", musica);
});

app.MapDelete("/musicas/{Id}", async (int Id, AppDbContext db) =>
{
    if (await db.Musicas.FindAsync(Id) is Musica musica)
    {
        db.Musicas.Remove(musica);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();
