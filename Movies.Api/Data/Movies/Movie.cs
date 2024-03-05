using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Api.Data.Movies;

[Table("Movies")]
public class Movie
{
    public int ID { get; set; }

    public string Title { get; set; } = default!;

    [Column("Release_Date")]
    public DateOnly ReleaseDate { get; set; }

    public string Overview { get; set; } = default!;

    public float Popularity { get; set; }

    [Column("Vote_Count")]
    public int VoteCount { get; set; }

    [Column("Vote_Average")]
    public float VoteAverage { get; set; }

    [Column("Original_Language")]
    public string OriginalLanguage { get; set; } = default!;

    public string Genre { get; set; } = default!;

    [Column("Poster_Url")]
    public string PosterUrl { get; set; } = default!;
}
