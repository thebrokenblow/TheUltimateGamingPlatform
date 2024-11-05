using Microsoft.EntityFrameworkCore;
using TheUltimateGamingPlatform.Model;
using TheUltimateGamingPlatform.Database.Repository.Interfaces;

namespace TheUltimateGamingPlatform.Database.Repository;

public class RepositoryGame(TheUltimateGamingPlatformContext context) : IRepositoryGame
{
    public async Task<List<Game>> GetAllAsync() =>
        await context.Games.ToListAsync();

    public async Task<Game> GetByIdAsync(int id) =>
        await context.Games.SingleAsync(game => game.Id == id);

    public async Task<Game> GetDetailsAsync(int id) =>
        await context.Games
            .Include(game => game.Genres)
            .Include(game => game.GameContents)
            .Include(game => game.MinimumSystemRequirement)
            .Include(game => game.RecommendedSystemRequirement)
            .SingleAsync(game => game.Id == id);
}