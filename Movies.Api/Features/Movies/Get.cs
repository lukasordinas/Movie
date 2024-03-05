using Microsoft.EntityFrameworkCore;
using Movies.Api.Core;
using Movies.Api.Data.Movies;

namespace Movies.Api.Features.Movies;

public class Get : IEndpoint
{
    internal const int MaxPageSize = 25;

    void IEndpoint.Map(WebApplication app)
    {
        app.MapGet("/v1/movies",
                   async ([AsParameters] DTOs.MovieCriteria requestDto,
                   Data.Movies.DbContext dbContext) => await HandleAsync(requestDto, dbContext))
            .WithSummary("Gets a paginated list of movies that match the criteria.")
            .WithOpenApi()
            .Produces<IEnumerable<Movie>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    internal static async Task<IResult> HandleAsync(DTOs.MovieCriteria requestDto, Data.Movies.DbContext dbContext)
    {
        try
        {
            var pageNumber = Math.Max(1, requestDto.PageNumber);
            var pageSize = Math.Max(1, Math.Min(MaxPageSize, requestDto.PageSize));

            var nameQuery = (requestDto.Title ?? string.Empty).ToLower();
            var genreQuery = (requestDto.Genre ?? string.Empty).ToLower();

            var query = dbContext.Movies.Where(m => m.Title.ToLower().Contains(nameQuery))
                                               .Where(m => m.Genre.ToLower().Contains(genreQuery))
                                               .AsQueryable();

            var movies = await DTOs.MoviePagedResponse.CreateAsync(pageNumber, pageSize, query);

            return Results.Ok(movies);
        }
        catch
        {
            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "Unexpected exception.");
        }
    }
}