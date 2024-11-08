using TheUltimateGamingPlatform.Model;
using TheUltimateGamingPlatform.Database.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheUltimateGamingPlatform.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using TheUltimateGamingPlatform.Database;

namespace TheUltimateGamingPlatform.Web.Pages;

public class GameDetailsModel(IRepositoryGame repositoryGame, IRepositoryUser repositoryUser, CartGameRepository cartGameRepository, TheUltimateGamingPlatformContext context) : PageModel
{
    public Game? Game { get; set; }
    public bool IsContainsInCart { get; set; }
    public bool IsPurchased { get; set; }

    public async Task OnGetAsync(int id)
    {
        IsContainsInCart = cartGameRepository.Games
            .Where(x => x.Id == id)
            .Any();

        IsPurchased = await repositoryUser.IsPurchasedGameAsync(id, 1);

        Game = await repositoryGame.GetDetailsAsync(id);
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var game = await repositoryGame.GetByIdAsync(id);
        cartGameRepository.Games.Add(game);

        return RedirectToPage("/Cart");
    }

    public async Task<IActionResult> OnPostAddWishList(int id)
    {
        await repositoryUser.AddGameWishListAsync(id, 1);

        return RedirectToPage("/Shop/WishList");
    }
}