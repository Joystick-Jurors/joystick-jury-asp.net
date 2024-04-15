using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JoystickJury.Data;
using JoystickJury.Models;

namespace JoystickJury.Pages.Games;

public class DetailsModel : PageModel
{
    private readonly JoystickJury.Data.ApplicationDbContext _context;

    public DetailsModel(JoystickJury.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public Game Game { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game.FirstOrDefaultAsync(m => m.Id == id);
        if (game == null)
        {
            return NotFound();
        }
        else
        {
            Game = game;
        }
        return Page();
    }
}