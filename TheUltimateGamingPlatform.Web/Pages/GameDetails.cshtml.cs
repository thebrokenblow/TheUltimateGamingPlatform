using TheUltimateGamingPlatform.Model;
using TheUltimateGamingPlatform.Database.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheUltimateGamingPlatform.Web.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace TheUltimateGamingPlatform.Web.Pages;

public class GameDetailsModel(IRepositoryGame repositoryGame, CartGameRepository cartGameRepository) : PageModel
{
    public Game? Game { get; set; }
    public bool IsContainsInCart { get; set; }

    public async Task OnGetAsync(int id)
    {
        IsContainsInCart = cartGameRepository.Games
            .Where(x => x.Id == id)
            .Any();

        Game = await repositoryGame.GetDetailsAsync(id);
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var game = await repositoryGame.GetByIdAsync(id);
        cartGameRepository.Games.Add(game);

        return RedirectToPage("/Cart");
    }
}