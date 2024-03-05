using Microsoft.EntityFrameworkCore;

namespace Movies.Api.Features.Movies.DTOs;

public record MovieCriteria(string? Title, string? Genre, int PageNumber = 1, int PageSize = Get.MaxPageSize);

public record Movie(int ID, string Title, DateOnly ReleaseDate, string Overview, float VoteAverage, string OriginalLanguage, string Genre)
{
    public static Movie Create(Data.Movies.Movie movie)
    {
        return new Movie(movie.ID,
            movie.Title,
            movie.ReleaseDate,
            movie.Overview,
            movie.VoteAverage,
            movie.OriginalLanguage,
            movie.Genre);
    }
}

public record MoviePagedResponse(int PageNumber, int PageSize, int TotalItems, IEnumerable<Movie> Items)
{
    public static async Task<MoviePagedResponse> CreateAsync(int pageNumber, int pageSize, IQueryable<Data.Movies.Movie> query)
    {
        var totalItems = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var DTOs = items.Select(Movie.Create).ToList();

        return new MoviePagedResponse(pageNumber, pageSize, totalItems, DTOs);
    }
}