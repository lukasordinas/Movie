using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Movies.Api.Features.Movies;
using Movies.Api.Features.Movies.DTOs;

namespace Movies.Api.Tests.Features.Movies;

public class GetTests
{
    [Test]
    public async Task HandleAsync_returns_internal_server_error_when_an_exception_is_thrown()
    {
        // Arrange
        var requestDto = CreateRequestDto();
        var dbContextOptions = new DbContextOptions<Data.Movies.DbContext>();
        var mockDbContext = new Mock<Data.Movies.DbContext>(dbContextOptions);
        mockDbContext.Setup(c => c.Movies).Throws(new InvalidOperationException("TestException"));

        // Act
        var result = await Get.HandleAsync(requestDto, mockDbContext.Object) as ProblemHttpResult;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
    }

    [Test]
    public async Task HandleAsync_returns_ok_and_a_paginated_list_of_movies_when_the_query_is_valid()
    {
        // Arrange
        var expectedItems = 10;

        var requestDto = CreateRequestDto(pageSize: expectedItems);
        var dbContextOptions = new DbContextOptions<Data.Movies.DbContext>();
        var mockDbContext = new Mock<Data.Movies.DbContext>(dbContextOptions);
        mockDbContext.Setup(c => c.Movies).ReturnsDbSet(CreateMovieSet(movieCount: expectedItems * 2));

        // Act
        var result = await Get.HandleAsync(requestDto, mockDbContext.Object) as Ok<MoviePagedResponse>;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        Assert.That(result.Value, Is.Not.Null);
        Assert.That(result.Value.Items.Count(), Is.EqualTo(expectedItems));
    }

    private static MovieCriteria CreateRequestDto(int pageNumber = 1, int pageSize = 10, string title = "", string genre = "")
    {
        return new MovieCriteria(title, genre, pageNumber, pageSize);
    }

    private static IEnumerable<Data.Movies.Movie> CreateMovieSet(int movieCount = 20)
    {
        var movies = new List<Data.Movies.Movie>();
        for (int i = 1; i <= movieCount; i++)
        {
            movies.Add(CreateMovie(i, $"TestMovie{i}", $"TestGenre{i}"));
        }

        return movies;
    }

    private static Data.Movies.Movie CreateMovie(int id, string title, string genre)
    {
        return new Data.Movies.Movie
        {
            ID = id,
            Title = title,
            Genre = genre
        };
    }
}