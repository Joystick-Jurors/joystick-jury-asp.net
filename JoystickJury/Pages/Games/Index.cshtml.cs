using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JoystickJury.Data;
using JoystickJury.Models;

namespace JoystickJury.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly JoystickJury.Data.ApplicationDbContext _context;

        public IndexModel(JoystickJury.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Game = await _context.Game.ToListAsync();
        }
    }
}
