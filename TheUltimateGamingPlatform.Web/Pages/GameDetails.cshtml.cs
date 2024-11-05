using TheUltimateGamingPlatform.Model;
using TheUltimateGamingPlatform.Database.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheUltimateGamingPlatform.Web.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TheUltimateGamingPlatform.Database;
using Microsoft.EntityFrameworkCore;

namespace TheUltimateGamingPlatform.Web.Pages;

public class GameDetailsModel(IRepositoryGame repositoryGame, CartGameRepository cartGameRepository, TheUltimateGamingPlatformContext context) : PageModel
{
    public Game? Game { get; set; }
    public bool IsContainsInCart { get; set; }
    public bool IsPurchased { get; set; }

    public async Task OnGetAsync(int id)
    {
        IsContainsInCart = cartGameRepository.Games
            .Where(x => x.Id == id)
            .Any();

        IsPurchased = await context.Carts
            .Include(x => x.User)
            .Include(x => x.Games)
            .Where(cart => cart.Games
                            .Select(x => x.Id)
                            .Contains(id))
            .Where(cart => cart.User.Id == 1)
            .AnyAsync();

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
        var game = await context.Games.Include(game => game.Users).SingleAsync(game => game.Id == id);
        var user = await context.Users.Include(user => user.Games).SingleAsync(user => user.Id == 1);

        game.Users.Add(user);
        user.Games.Add(game);

        await context.SaveChangesAsync();

        return RedirectToPage("/Shop/WishList");
    }
}