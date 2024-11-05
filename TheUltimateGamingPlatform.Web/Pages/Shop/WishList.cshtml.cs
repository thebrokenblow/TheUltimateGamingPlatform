using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheUltimateGamingPlatform.Database;
using TheUltimateGamingPlatform.Model;

namespace TheUltimateGamingPlatform.Web.Pages.Shop;

public class WishListModel(TheUltimateGamingPlatformContext context) : PageModel
{
    public List<Game>? Games { get; set; }

    public async Task OnGetAsync()
    {
        var user = await context.Users
            .Include(user => user.Games)
            .SingleAsync(user => user.Id == 1);

        Games = user.Games;
    }
}
