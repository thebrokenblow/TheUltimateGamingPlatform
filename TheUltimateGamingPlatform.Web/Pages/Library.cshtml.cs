using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheUltimateGamingPlatform.Database;
using TheUltimateGamingPlatform.Model;

namespace TheUltimateGamingPlatform.Web.Pages;

public class LibraryModel(TheUltimateGamingPlatformContext context) : PageModel
{
    public List<Game> Games { get; set; } = [];

    public async Task OnGetAsync()
    {
        var games = await context.Carts
            .Include(cart => cart.User)
            .Include(cart => cart.Games)
            .Where(cart => cart.User.Id == 1)
            .Select(cart => cart.Games)
            .ToListAsync();

        foreach (var game in games)
        {
            Games.AddRange(game);
        }
    }
}