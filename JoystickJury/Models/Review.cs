using System.ComponentModel.DataAnnotations;

namespace JoystickJury.Models;

public class Review
{
	public int Id { get; set; }

	[Required, StringLength(100)]
	public string Title { get; set; }

	[Required, StringLength(5000)]
	public string Body { get; set; }

	[Required, Display(Name="Rating (# Stars)")]
	public string StarRating { get; set; }

	public int Likes { get; set; }

	public int Dislikes { get; set; }

	public int CommunityRating => Likes - Dislikes;

	public DateTimeOffset LastUpdated { get; set; }

	public Game Game { get; set; }

	public ApplicationUser User { get; set; }

	// TODO: Eventually, comments
}