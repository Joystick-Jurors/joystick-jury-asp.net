using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JoystickJury.Data;
using JoystickJury.Models;
using JoystickJury.Helpers;

namespace JoystickJury.Pages.Games;

public class CreateModel : PageModel
{
    private readonly JoystickJury.Data.ApplicationDbContext _context;

    private readonly IConfiguration _config;

    public CreateModel(JoystickJury.Data.ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty] public Game Game { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Fetch the game based on an ID...
        //var gameId = Game.IgdbId;

        // Get API keys from project secrets - see https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets
        var clientId = _config["IGDB:ClientID"];
        var clientSecret = _config["IGDB:ClientSecret"];

        // Authenticate (get an access token)
        if (clientId == null || clientSecret == null)
        {
            throw new NullReferenceException("IGDB (Twitch) Client ID and/or secret were not configured properly.");
        }

        var authToken = await IGDB.TwitchAuth(clientId, clientSecret) ??
                        throw new NullReferenceException("IGDB (Twitch) authentication failed for some reason.");

        // Now fetch the game
        var gameResponse = await IGDB.FetchGame(Game.IgdbId, clientId, authToken) ??
                           throw new NullReferenceException(
                               $"Game fetch for #{Game.IgdbId} failed for some reason.");

        // Build Game from response
        Game.Name = gameResponse.name;
        Game.IgdbCover = gameResponse.cover.image_id;
        //Game.Genres = await IGDBRequests.FetchNames(gameResponse.genres, "genres", clientId, authToken);
        //Game.Platforms = await IGDBRequests.FetchNames(gameResponse.platforms, "platforms", clientId, authToken);
        Game.Genres = string.Join("\n", gameResponse.genres.Select(x => x.name));
        Game.Platforms = string.Join("\n", gameResponse.platforms.Select(x => x.name));
        Game.IgdbLastUpdate = DateTimeOffset.FromUnixTimeSeconds(gameResponse.updated_at);
        Game.ReleaseDate = DateTimeOffset.FromUnixTimeSeconds(gameResponse.first_release_date);

        _context.Game.Add(Game);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}