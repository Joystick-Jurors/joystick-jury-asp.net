using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JoystickJury.Data;
using JoystickJury.Models;
using Microsoft.AspNetCore.Identity;

namespace JoystickJury.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly JoystickJury.Data.ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> UserManager;

        public IndexModel(UserManager<ApplicationUser> userManager, JoystickJury.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Review> Review { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Review = await _context.Review.ToListAsync();
        }
    }
}
