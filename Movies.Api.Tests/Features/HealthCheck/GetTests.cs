using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Movies.Api.Tests.Features.HealthCheck;

[TestFixture]
internal class GetTests
{
    [Test]
    public void Handle_returns_ok()
    {
        // Act
        var result = Api.Features.HealthCheck.Get.Handle() as Ok<string>;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        Assert.That(result.Value, Is.EqualTo("Healthy"));
    }
}