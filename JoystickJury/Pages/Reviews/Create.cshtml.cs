using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JoystickJury.Data;
using JoystickJury.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;

namespace JoystickJury.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly JoystickJury.Data.ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> UserManager;

		public CreateModel(UserManager<ApplicationUser> userManager, JoystickJury.Data.ApplicationDbContext context)
        {
            UserManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Review Review { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // If rating is null, make it 0
            var stars = HttpContext.Request.Form["StarRating"];
            if (StringValues.IsNullOrEmpty(stars)) Review.StarRating = "0";

            // whyyyy is this necessaryyyyy
            Review.StarRating = stars;

			Review.LastUpdated = DateTimeOffset.Now;

            // Gets the logged-in user
            Review.Author = await UserManager.GetUserAsync(HttpContext.User);

            _context.Review.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
