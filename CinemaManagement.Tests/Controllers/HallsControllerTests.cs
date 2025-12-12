using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Tests.Controllers;

public class HallsControllerTests
{
    private CinemaContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<CinemaContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new CinemaContext(options);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfHalls()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Halls.AddRange(
            new Hall { Id = 1, Name = "Hall 1", Rows = 10, SeatsPerRow = 10 },
            new Hall { Id = 2, Name = "Hall 2", Rows = 15, SeatsPerRow = 10 }
        );
        await context.SaveChangesAsync();

        var controller = new HallsController(context);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Hall>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Create_Post_AddsHallAndRedirects()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new HallsController(context);
        var newHall = new Hall { Name = "Hall 3", Rows = 20, SeatsPerRow = 10 };

        // Act
        var result = await controller.Create(newHall);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal(1, await context.Halls.CountAsync());
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new HallsController(context);

        // Act
        var result = await controller.Details(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithHall()
    {
        // Arrange
        var context = GetInMemoryContext();
        var hall = new Hall { Id = 1, Name = "Test Hall", Rows = 10, SeatsPerRow = 10 };
        context.Halls.Add(hall);
        await context.SaveChangesAsync();

        var controller = new HallsController(context);

        // Act
        var result = await controller.Details(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Hall>(viewResult.Model);
        Assert.Equal("Test Hall", model.Name);
        Assert.Equal(100, model.Capacity);
    }
}
