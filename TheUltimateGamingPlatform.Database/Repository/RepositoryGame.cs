using Microsoft.EntityFrameworkCore;
using TheUltimateGamingPlatform.Model;
using TheUltimateGamingPlatform.Database.Repository.Interfaces;

namespace TheUltimateGamingPlatform.Database.Repository;

public class RepositoryGame(TheUltimateGamingPlatformContext context) : IRepositoryGame
{
    public async Task<List<Game>> GetAllAsync() =>
        await context.Games.ToListAsync();

    public async Task<Game> GetDetailsAsync(int id)
    {
        return await context.Games
                            .Include(game => game.Genres)
                            .SingleAsync(game => game.Id == id);
    }
}