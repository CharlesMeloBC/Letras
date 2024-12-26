using Letrasdemusicas.Data;
using Letrasdemusicas.Models;
using Microsoft.EntityFrameworkCore;

public interface IMusicaRepository
{
    Task<IEnumerable<Musica>> GetAllAsync();
    Task<Musica?> GetByIdAsync(int id);
    Task<IEnumerable<Musica>> SearchAsync(string filter);
    Task AddAsync(Musica musica);
    Task<bool> DeleteAsync(int id);
}

public class MusicaRepository : IMusicaRepository
{
    private readonly AppDbContext _context;

    public MusicaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Musica>> GetAllAsync() =>
        await _context.Musicas.ToListAsync();

    public async Task<Musica?> GetByIdAsync(int id) =>
        await _context.Musicas.FindAsync(id);

    public async Task<IEnumerable<Musica>> SearchAsync(string filter) =>
        await _context.Musicas
            .Where(m => m.Name.ToLower().Contains(filter.ToLower()) ||
                        m.Letra.ToLower().Contains(filter.ToLower()))
            .ToListAsync();

    public async Task AddAsync(Musica musica)
    {
        _context.Musicas.Add(musica);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var musica = await _context.Musicas.FindAsync(id);
        if (musica is null) return false;

        _context.Musicas.Remove(musica);
        await _context.SaveChangesAsync();
        return true;
    }
}
