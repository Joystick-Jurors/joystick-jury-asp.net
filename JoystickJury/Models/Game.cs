using System.ComponentModel.DataAnnotations;

namespace JoystickJury.Models;

public class Game
{
	public int Id { get; set; }

	public int IgdbId { get; set; }
	public int IgdbCover { get; set; }

	[DataType(DataType.Date)]
	public DateTimeOffset ReleaseDate { get; set; }

	public string? Name { get; set; }
	public string? Genres { get; set; }
	public string? Platforms { get; set; }
}