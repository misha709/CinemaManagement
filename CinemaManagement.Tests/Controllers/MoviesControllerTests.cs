using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Tests.Controllers;

public class MoviesControllerTests
{
    private CinemaContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<CinemaContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new CinemaContext(options);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfMovies()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Movies.AddRange(
            new Movie { Id = 1, Title = "Test Movie 1", DurationMinutes = 120, Genre = "Action" },
            new Movie { Id = 2, Title = "Test Movie 2", DurationMinutes = 90, Genre = "Comedy" }
        );
        await context.SaveChangesAsync();

        var controller = new MoviesController(context);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Movie>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new MoviesController(context);

        // Act
        var result = await controller.Details(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenMovieDoesNotExist()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new MoviesController(context);

        // Act
        var result = await controller.Details(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithMovie()
    {
        // Arrange
        var context = GetInMemoryContext();
        var movie = new Movie { Id = 1, Title = "Test Movie", DurationMinutes = 120, Genre = "Drama" };
        context.Movies.Add(movie);
        await context.SaveChangesAsync();

        var controller = new MoviesController(context);

        // Act
        var result = await controller.Details(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Movie>(viewResult.Model);
        Assert.Equal("Test Movie", model.Title);
    }

    [Fact]
    public async Task Create_Post_AddsMovieAndRedirects()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new MoviesController(context);
        var newMovie = new Movie { Title = "New Movie", DurationMinutes = 100, Genre = "Thriller" };

        // Act
        var result = await controller.Create(newMovie);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal(1, await context.Movies.CountAsync());
    }

    [Fact]
    public async Task Create_Post_ReturnsView_WhenModelStateIsInvalid()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new MoviesController(context);
        controller.ModelState.AddModelError("Title", "Required");
        var movie = new Movie();

        // Act
        var result = await controller.Create(movie);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(movie, viewResult.Model);
    }

    [Fact]
    public async Task Delete_Post_RemovesMovieAndRedirects()
    {
        // Arrange
        var context = GetInMemoryContext();
        var movie = new Movie { Id = 1, Title = "To Delete", DurationMinutes = 105, Genre = "Horror" };
        context.Movies.Add(movie);
        await context.SaveChangesAsync();

        var controller = new MoviesController(context);

        // Act
        var result = await controller.DeleteConfirmed(1);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal(0, await context.Movies.CountAsync());
    }
}
