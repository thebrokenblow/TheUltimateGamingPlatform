using Microsoft.AspNetCore.Mvc.RazorPages;
using TheUltimateGamingPlatform.Database.Repository.Interfaces;
using TheUltimateGamingPlatform.Model;
using TheUltimateGamingPlatform.Web.Repositories;

namespace TheUltimateGamingPlatform.Web.Pages;

public class CartModel(IRepositoryGame repositoryGame, CartGameRepository cartGameRepository) : PageModel
{
    public List<Game>? Games { get; set; }
    public void OnGet()
    {
        Games = cartGameRepository.Games;
    }

    public void OnPost(int id)
    {
        var game = cartGameRepository.Games.Single(x => x.Id == id);
        cartGameRepository.Games.Remove(game);
    }
}
