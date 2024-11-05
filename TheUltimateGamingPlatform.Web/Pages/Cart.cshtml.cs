using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheUltimateGamingPlatform.Database;
using TheUltimateGamingPlatform.Database.Repository.Interfaces;
using TheUltimateGamingPlatform.Model;
using TheUltimateGamingPlatform.Web.Repositories;

namespace TheUltimateGamingPlatform.Web.Pages;

public class CartModel(
    IRepositoryGame repositoryGame, 
    CartGameRepository cartGameRepository, 
    TheUltimateGamingPlatformContext theUltimateGamingPlatformContext) : PageModel
{
    public List<Game>? Games { get; set; }
    public decimal SumProduct { get; set; }

    private User? user;
    public async Task OnGetAsync()
    {
        user = await theUltimateGamingPlatformContext.Users.SingleAsync(user => user.Id == 1);
        Games = cartGameRepository.Games;
        SumProduct = cartGameRepository.Games.Sum(game => game.Price);
    }

    public void OnPostDeleteGame(int id)
    {
        var game = cartGameRepository.Games.Single(game => game.Id == id);
        cartGameRepository.Games.Remove(game);
    }

    public async Task<IActionResult> OnPostCreateOrder()
    {
        var games = await theUltimateGamingPlatformContext.Games
            .Where(game => cartGameRepository.Games
                    .Select(game => game.Id)
                    .Contains(game.Id))
            .ToListAsync();
        
        var cart = new Cart
        {
            DatePurchase = DateOnly.FromDateTime(DateTime.Now),
            Sum = cartGameRepository.Games.Sum(game => game.Price),
            User = await theUltimateGamingPlatformContext.Users.SingleAsync(user => user.Id == 1),
            Games = games
        };

        await theUltimateGamingPlatformContext.Carts.AddAsync(cart);
        await theUltimateGamingPlatformContext.SaveChangesAsync();

        cartGameRepository.Games.Clear();
        
        return RedirectToPage("/Index");
    }
}