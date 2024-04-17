using Microsoft.AspNetCore.Identity;

namespace JoystickJury.Models;

public class ApplicationUser : IdentityUser
{
    // IdentityUser has username, email, phone

    // Public-facing name, like Twitter/Discord
    public string Nickname { get; set; }

    // This is one of the image storage solutions of all time
    public byte[]? ProfilePicture { get; set; }

    public ICollection<Review> Reviews { get; set; }
}