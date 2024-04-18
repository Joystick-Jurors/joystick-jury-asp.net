using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JoystickJury.Helpers;

namespace JoystickJury.Models;

public class Game
{
	public int Id { get; set; }

	[Display(Name="IGDB #")]
	public int IgdbId { get; set; }

	// cover art ID
	[Display(Name="IGDB Cover Hash")]
	public string? IgdbCover { get; set; }

    [NotMapped, Display(Name="Cover")]
    public string SmallCover => IGDB.BuildCoverUrl(IgdbCover, "small");

	[NotMapped, Display(Name="Cover")]
    public string LargeCover => IGDB.BuildCoverUrl(IgdbCover, "big");

	// "updated_at" field for determining whether an update is needed
	// TODO: use this
	[Display(Name="Last Updated on IGDB")]
	public DateTimeOffset IgdbLastUpdate { get; set; }

	[Display(Name="Release Date"), DataType(DataType.Date)]
	public DateTimeOffset ReleaseDate { get; set; }

	public string? Name { get; set; }
	public string? Genres { get; set; }
	public string? Platforms { get; set; }

	public ICollection<Review>? Reviews { get; set; }
}