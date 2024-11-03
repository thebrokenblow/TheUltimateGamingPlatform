using TheUltimateGamingPlatform.Model;

namespace TheUltimateGamingPlatform.Database.Repository.Interfaces;

public interface IRepositoryGame
{
    public Task<List<Game>> GetAllAsync();
    public Task<Game> GetDetailsAsync(int id);
}